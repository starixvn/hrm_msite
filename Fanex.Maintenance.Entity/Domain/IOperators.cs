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
    public interface IOperators : IEntity<int, string>
    {
        /// <summary>
        /// Status of alert notifications (Boolean). If 1, this operator can receive notifications when an alert occurs.
        /// </summary>
        int Enable { get; set; }

        /// <summary>
        /// E-mail address for this operator.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Date this operator last received an e-mail alert notification.
        /// </summary>
        int LastEmailDate { get; set; }

        /// <summary>
        /// Time of day this operator last received an e-mail alert notification.
        /// </summary>
        int LastEmailTime { get; set; }

        /// <summary>
        /// Pager address for this operator.
        /// </summary>
        string PagerAddress { get; set; }

        /// <summary>
        /// Date this operator last received a pager alert notification.
        /// </summary>
        int LastPagerTime { get; set; }

        /// <summary>
        /// Time of day this operator last received a pager alert notification.
        /// </summary>
        int WeekdayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on a weekday (Monday through Friday) after which this operator is available to receive a pager alert notification.
        /// </summary>
        int WeekdayPagerEndTime { get; set; }

        /// <summary>
        /// Time of day on a weekday (Monday through Friday) after which this operator is not available to receive a pager alert notification.
        /// </summary>
        int SaturdayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on Saturday after which this operator is available to receive a pager alert notification.
        /// </summary>
        int SaturdayPagerEndTime { get; set; }

        /// <summary>
        /// Time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        int SundayPagerStartTime { get; set; }

        /// <summary>
        /// Time of day on Sunday after which this operator is available to receive a pager alert notification.
        /// </summary>
        int SundayPagerEndTime { get; set; }

        /// <summary>
        /// Bitmask representing the days of the week during which this operator is available to receive a pager alert notification.
        /// </summary>
        int PagerDay { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        int NetsendAddress { get; set; }

        /// <summary>
        /// Date that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        int LastNetsendDate { get; set; }

        /// <summary>
        /// Time that the most recent network message was last sent to the specified operator ID.
        /// </summary>
        int LastNetsendTime { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        int CategoryId { get; set; }
    }
}
