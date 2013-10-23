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
        /// Gets or sets status of alert notifications (Boolean). If 1, this operator can receive notifications when an alert occurs.
        /// </summary>
        public int Enable { get; set; }

        public string Email { get; set; }

        public int LastEmailDate { get; set; }

        public int LastEmailTime { get; set; }

        public string PagerAddress { get; set; }

        /// <summary>
        /// Gets or sets date this operator last received a pager alert notification.
        /// </summary>
        public int LastPagerTime { get; set; }

        public int WeekdayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on a weekday (Monday through Friday) after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int WeekdayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on a weekday (Monday through Friday) after which this operator is not available to receive a pager alert notification.
        /// </summary>
        public int SaturdayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Saturday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SaturdayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SundayPagerStartTime { get; set; }

        /// <summary>
        /// Gets or sets time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        public int SundayPagerEndTime { get; set; }

        /// <summary>
        /// Gets or sets bitmask representing the days of the week during which this operator is available to receive a pager alert notification.
        /// </summary>
        public int PagerDay { get; set; }

        /// <summary>
        /// Gets or sets identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int NetsendAddress { get; set; }

        /// <summary>
        /// Gets or sets date that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        public int LastNetsendDate { get; set; }

        /// <summary>
        /// Gets or sets time that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        public int LastNetsendTime { get; set; }

        /// <summary>
        /// Gets or sets identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int CategoryId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
