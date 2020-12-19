using System;
using System.Collections.Generic;
using Formly.Server.Services;
using NUnit.Framework;

namespace Formly.Server.Tests.Services
{
  [TestFixture]
  public class TemplateProcessorTests
  {
    [Test]
    public void TestGetMetaDataItems_OnMultipleLines()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      var items = templateProcessor.GetMetaDataItems($"**First Name:** {{{{FirstName}}}}{Environment.NewLine}**Last Name:** {{{{LastName}}}}");

      Assert.AreEqual(2, items.Count);
      Assert.AreEqual("FirstName", items[0].Name);
      Assert.AreEqual("LastName", items[1].Name);
    }

    [Test]
    public void TestGetMetaDataItems_WhenContainsOnlyPlaceholder()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      var items = templateProcessor.GetMetaDataItems("{{FirstName}}");

      Assert.AreEqual(1, items.Count);
      Assert.AreEqual("FirstName", items[0].Name);
    }

    [Test]
    public void TestGetMetaDataItems_WhenContainsSamePlaceHolderMultipleTimes()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      var items = templateProcessor.GetMetaDataItems("**{{FirstName}}**{{LastName}} {{FirstName}}");

      Assert.AreEqual(2, items.Count);
      Assert.AreEqual("FirstName", items[0].Name);
      Assert.AreEqual("LastName", items[1].Name);
    }

    [Test]
    public void TestTransformToText_Success()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      IDictionary<string, string> placeholderValues = new Dictionary<string, string>
      {
        ["FirstName"] = "Mo",
        ["LastName"] = "The Best"
      };
      string templateContent = "**{{FirstName}}** {{LastName}} {{FirstName}}";

      string transformedTemplate = templateProcessor.TransformToText(templateContent, placeholderValues);

      StringAssert.AreEqualIgnoringCase("<p><strong>Mo</strong> The Best Mo</p>\n", transformedTemplate);
    }

    [Test]
    public void TestTransformToPdf_Success()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      IDictionary<string, string> placeholderValues = new Dictionary<string, string>
      {
        ["FirstName"] = "Mo",
        ["LastName"] = "The Best"
      };
      string templateContent = @"#Contract Sales
                               **{{FirstName}}** {{LastName}} {{FirstName}}";

      string fileName = templateProcessor.TransformToPdf(templateContent, placeholderValues);
      Console.Out.WriteLine(fileName);
    }
  }
}
