﻿// ==========================================================================
//  ModelFieldAdded.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using PinkParrot.Core.Schema;
using PinkParrot.Infrastructure;

namespace PinkParrot.Events.Schema
{
    [TypeName("ModelFieldAddedEvent")]
    public class ModelFieldAdded : TenantEvent
    {
        public long FieldId { get; set; }

        public string Name { get; set; }

        public IModelFieldProperties Properties { get; set; }
    }
}
