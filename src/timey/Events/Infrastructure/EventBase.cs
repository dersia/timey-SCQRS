﻿using System;

namespace timey.Events.Infrastructure
{
    public abstract class EventBase
    {
        public Guid Id { get; set; }
        public string CLRType => GetType().FullName;
    }
}
