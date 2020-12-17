using System;
using Formly.Server.Services;
using NUnit.Framework;

namespace Formly.Server.Tests
{
  [TestFixture]
  public class TemplateMetaDataRetrieverTests
  {
    [Test]
    public void TestGetMetaDataItems_OnMultipleLines()
    {
      TemplateMetaDataRetriever retriever = new TemplateMetaDataRetriever();

      var items = retriever.GetMetaDataItems($"**First Name:** {{{{FirstName}}}}{Environment.NewLine}**Last Name:** {{{{LastName}}}}");

      Assert.AreEqual(2, items.Count);
      Assert.AreEqual("FirstName", items[0].Name);
      Assert.AreEqual("LastName", items[1].Name);
    }

    [Test]
    public void TestGetMetaDataItems_WhenContainsOnlyPlaceholder()
    {
      TemplateMetaDataRetriever retriever = new TemplateMetaDataRetriever();

      var items = retriever.GetMetaDataItems("{{FirstName}}");

      Assert.AreEqual(1, items.Count);
      Assert.AreEqual("FirstName", items[0].Name);
    }
  }
}
