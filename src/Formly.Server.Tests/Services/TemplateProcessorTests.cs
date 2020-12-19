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
    public void TestTransform_Success()
    {
      TemplateProcessor templateProcessor = new TemplateProcessor();

      string transformedTemplate = templateProcessor.Transform("**{{FirstName}}**", new Dictionary<string, string> { ["FirstName"] = "Mo" });

      StringAssert.AreEqualIgnoringCase("<p><strong>Mo</strong></p>\n", transformedTemplate);
    }
  }
}
