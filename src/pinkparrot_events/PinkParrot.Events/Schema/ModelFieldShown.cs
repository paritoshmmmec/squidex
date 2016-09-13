﻿// ==========================================================================
//  ModelFieldShown.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using PinkParrot.Infrastructure;

namespace PinkParrot.Events.Schema
{
    [TypeName("ModelFieldShownEvent")]
    public class ModelFieldShown : TenantEvent
    {
        public long FieldId { get; set; }
    }
}
