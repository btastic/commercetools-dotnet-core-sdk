﻿using System;
using System.Collections.Generic;
using System.Text;

namespace commercetools.Sdk.Domain
{
    public class SetExternalId : UpdateAction
    {
        public string Action => "setExternalId";
        public string ExternalId { get; set; }
    }
}
