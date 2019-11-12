﻿using commercetools.Sdk.Domain.Common;
using commercetools.Sdk.Domain.Types;

namespace commercetools.Sdk.Domain
{
    public class CustomFieldsDraft : IDraft<CustomFields>
    {
        public IReference<Type> Type { get; set; }
        public Fields Fields { get; set; }
    }
}
