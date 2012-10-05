// --------------------------------------------------------------------------
// <copyright file="Operators.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Operators : IOperators
    {
        public int Enable { get; set; }

        public string Email { get; set; }

        public int LastEmailDate { get; set; }

        public int LastEmailTime { get; set; }

        public string PagerAddress { get; set; }

        public int LastPagerTime { get; set; }

        public int WeekdayPagerStartTime { get; set; }

        public int WeekdayPagerEndTime { get; set; }

        public int SaturdayPagerStartTime { get; set; }

        public int SaturdayPagerEndTime { get; set; }

        public int SundayPagerStartTime { get; set; }

        public int SundayPagerEndTime { get; set; }

        public int PagerDay { get; set; }

        public int NetsendAddress { get; set; }

        public int LastNetsendDate { get; set; }

        public int LastNetsendTime { get; set; }

        public int CategoryId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
