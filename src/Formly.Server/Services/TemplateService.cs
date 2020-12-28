using System.Collections.Generic;
using System.Linq;
using Formly.DataEntities;
using Formly.Shared;
using Formly.Shared.Services;
using Framework;

namespace Formly.Server.Services
{
  internal class TemplateService : ITemplateService
  {
    private readonly FormlyDbContext mContext;
    private readonly ITemplateProcessor mTemplateProcessor;

    public TemplateService(FormlyDbContext context, ITemplateProcessor templateProcessor)
    {
      Check.Assigned(context, nameof(context));
      Check.Assigned(templateProcessor, nameof(templateProcessor));

      mContext = context;
      mTemplateProcessor = templateProcessor;
    }

    public string GetTemplateContent(long id)
    {
      return ActiveTemplates.Where(x => x.Id == id).Select(x => x.Content).SingleOrDefault();
    }

    public string GetTemplateContentByExternalId(string externalId)
    {
      return ActiveTemplates.Where(x => x.ExternalId == externalId).Select(x => x.Content).SingleOrDefault();
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long id)
    {
      string templateContent = GetTemplateContent(id);

      return mTemplateProcessor.GetMetaDataItems(templateContent);
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaDataByExternalId(string externalId)
    {
      string templateContent = GetTemplateContentByExternalId(externalId);

      return mTemplateProcessor.GetMetaDataItems(templateContent);
    }

    public string Transform(long id, IDictionary<string, string> placeholderValues)
    {
      string templateContent = GetTemplateContent(id);

      return mTemplateProcessor.TransformToText(templateContent, placeholderValues);
    }

    public TemplateDetails GetTemplateDetails(long templateId)
    {
      var query = mContext.Templates.Where(x => x.Id == templateId && x.IsActive).Select(x => MapTemplateToTemplateDetails(x));

      return query.SingleOrDefault();
    }

    public TemplateDetails GetTemplateDetailsByExternalId(string externalId)
    {
      return ActiveTemplates.Where(x => x.ExternalId == externalId).Select(x => MapTemplateToTemplateDetails(x)).SingleOrDefault();
    }

    public IList<TemplateDetails> GetAllTemplates()
    {
      return mContext.Templates.Where(x => x.IsActive).Select(x => MapTemplateToTemplateDetails(x)).ToList();
    }

    private static TemplateDetails MapTemplateToTemplateDetails(TemplateDataEntity templateDataEntity)
    {
      return new TemplateDetails
      {
        Name = templateDataEntity.Name,
        Description = templateDataEntity.Description,
        Id = templateDataEntity.Id,
        ExternalId = templateDataEntity.ExternalId,
        CreatedOn = templateDataEntity.CreatedOn,
        LastUpdatedOn = templateDataEntity.LastUpdatedOn,
        Tags = new List<string>()
      };
    }

    private IQueryable<TemplateDataEntity> ActiveTemplates => mContext.Templates.Where(x => x.IsActive);
  }
}
