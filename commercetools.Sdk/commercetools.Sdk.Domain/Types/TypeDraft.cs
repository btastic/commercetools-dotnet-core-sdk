﻿using System.Collections.Generic;

namespace commercetools.Sdk.Domain.Types
{
    public class TypeDraft : IDraft<Type>
    {
        public string Key { get; set; }
        public LocalizedString Name { get; set; }
        public LocalizedString Description { get; set; }
        public List<ResourceTypeId> ResourceTypeIds { get; set; }
        public List<FieldDefinition> FieldDefinitions { get; set; }
    }
}