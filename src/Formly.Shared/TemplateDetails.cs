﻿using System;
using System.Collections.Generic;

namespace Formly.Shared
{
  public class TemplateDetails
  {
    public long Id { get; set; }
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IList<string> Tags { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
  }
}