﻿// ==========================================================================
//  ModelFieldDisabled.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using PinkParrot.Infrastructure;

namespace PinkParrot.Events.Schema
{
    [TypeName("ModelFieldDisabledEvent")]
    public class ModelFieldDisabled : TenantEvent
    {
        public long FieldId { get; set; }
    }
}
