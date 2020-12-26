using System;
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

    public string GetTemplateContent(long templateId)
    {
      return mContext.Templates.SingleOrDefault(x => x.Id == templateId && x.IsActive).Content;
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long templateId)
    {
      string templateContent = GetTemplateContent(templateId);

      return mTemplateProcessor.GetMetaDataItems(templateContent);
    }

    public string Transform(long templateId, IDictionary<string, string> placeholderValues)
    {
      string templateContent = GetTemplateContent(templateId);

      return mTemplateProcessor.TransformToText(templateContent, placeholderValues);
    }

    public TemplateDetails GetTemplateDetails(long templateId)
    {
      var query = mContext.Templates.Where(x => x.Id == templateId && x.IsActive).Select(x => MapTemplateToTemplateDetails(x));

      return query.SingleOrDefault();
    }

    private static TemplateDetails MapTemplateToTemplateDetails(TemplateDataEntity templateDataEntity)
    {
      return new TemplateDetails
      {
        Name = templateDataEntity.Name,
        Description = templateDataEntity.Description,
        Id = templateDataEntity.Id,
        Tags = new List<string>()
      };
    }
  }
}
