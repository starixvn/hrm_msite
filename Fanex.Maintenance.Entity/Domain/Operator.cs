// --------------------------------------------------------------------------
// <copyright file="IOperators.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class Operator : IEntity<int, string>
    {
        /// <summary>
        /// Status of alert notifications (Boolean). If 1, this operator can receive notifications when an alert occurs.
        /// </summary>
        public int Enable { get; set; }

        /// <summary>
        /// E-mail address for this operator.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Date this operator last received an e-mail alert notification.
        /// </summary>
        public int LastEmailDate { get; set; }

        /// <summary>
        /// Time of day this operator last received an e-mail alert notification.
        /// </summary>
        public int LastEmailTime { get; set; }

        /// <summary>
        /// Pager address for this operator.
        /// </summary>
        public string PagerAddress { get; set; }

        /// <summary>
        /// Date this operator last received a pager alert notification.
        /// </summary>
        public int LastPagerTime { get; set; }

        /// <summary>
        /// Time of day this operator last received a pager alert notification.
        /// </summary>
        public int WeekdayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on a weekday (Monday through Friday) after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int WeekdayPagerEndTime { get; set; }

        /// <summary>
        /// Time of day on a weekday (Monday through Friday) after which this operator is not available to receive a pager alert notification.
        /// </summary>
        public int SaturdayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on Saturday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SaturdayPagerEndTime { get; set; }

        /// <summary>
        /// Time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SundayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SundayPagerEndTime { get; set; }

        /// <summary>
        /// Bitmask representing the days of the week during which this operator is available to receive a pager alert notification.
        /// </summary>
        public int PagerDay { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int NetsendAddress { get; set; }

        /// <summary>
        /// Date that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        public int LastNetsendDate { get; set; }

        /// <summary>
        /// Time that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        public int LastNetsendTime { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int CategoryId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
