﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBattle.Models.Areas
{
    public class AreasListViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }
        public int MinLevel { get; init; }
        public int MaxLevel { get; init; }
        public string Description { get; init; }
        public int CraturesCount { get; init; }
    }
}
