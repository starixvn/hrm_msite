// --------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    /// <summary>
    /// Mail operator.
    /// </summary>
    public class Operator : IEntity<int, string>
    {
        /// <summary>
        /// Gets or sets status of alert notifications (Boolean).
        /// </summary>
        /// <value>1 : this operator can receive notifications when an alert occurs.</value>
        public int Enable { get; set; }

        public string Email { get; set; }

        public int LastEmailDate { get; set; }

        public int LastEmailTime { get; set; }

        public string PagerAddress { get; set; }

        public int LastPagerTime { get; set; }

        public int WeekdayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on a weekday after which this operator is available to receive a pager alert notification.
        /// </summary>
        /// <value>Monday through Friday.</value>
        public int WeekdayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on a weekday after which this operator is not available to receive a pager alert notification.
        /// </summary>
        /// <value>Monday through Friday.</value>
        public int SaturdayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Saturday after which this operator is available to receive a pager alert notification.
        /// </summary>
        /// <value>Saturday pager end time value.</value>
        public int SaturdayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        /// <value>Sunday pager start time value.</value>
        public int SundayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        /// <value>Sunday pager end time value.</value>
        public int SundayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets bitmask representing the days of the week during which this operator is available to receive a pager alert notification.
        /// </summary>
        /// <value>Pager day bitmask.</value>
        public int PagerDay { get; set; }

        public int NetsendAddress { get; set; }

        /// <summary>
        /// Gets or sets date that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        /// <value>Last net send date.</value>
        public int LastNetsendDate { get; set; }

        /// <summary>
        /// Gets or sets time that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        /// <value>Last net send time.</value>
        public int LastNetsendTime { get; set; }

        public int CategoryId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
