﻿using System;
using System.Collections.Generic;
using System.Text;

namespace commercetools.Sdk.Domain.Categories
{
    public class RemoveAssetUpdateAction : UpdateAction<Category>
    {
        public string Action => "removeAsset";
        public string AssetId { get; set; }
        public string AssetKey { get; set; }
    }
}