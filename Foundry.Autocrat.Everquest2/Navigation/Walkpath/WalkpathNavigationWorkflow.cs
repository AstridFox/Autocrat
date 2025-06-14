using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Geometry;
using Foundry.Autocrat.Automation.Windows;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath
{

    /// <summary>
    /// Workflow responsible for navigating the character through a given walkpath.
    /// </summary>
    public class WalkpathNavigationWorkflow : Workflow
    {
        private WalkpathNavigationContext Context;

        public WalkpathNavigationWorkflow(WalkpathNavigationContext context)
        {
            Context = context;
        }

        // Input
        public bool NavigationInterrupted { private get; set; }

        // Output
        public Waypoint CurrentWaypoint { get; private set; }

        protected override IEnumerable<WorkItem> WorkFlow()
        {
            for (int i = 0; i < Context.Walkpath.Waypoints.Count; i++)
            {
                if (NavigationInterrupted)
                    i = Context.GetClosestWaypointIndex(i, Context.Walkpath.Waypoints);

                CurrentWaypoint = Context.Walkpath.Waypoints[i];
                yield return WorkItems.WaypointChanged.Singleton;

                var walk = new WorkItems.WalkToWaypoint
                {
                    Context = Context,
                    Destination = CurrentWaypoint
                };

                do
                {
                    if (NavigationInterrupted) continue;
                    yield return walk;
                } while (!walk.ReachedDestination);

                if (Context.JumpingAllowed && CurrentWaypoint.JumpWhenReached)
                {
                    yield return new WorkItems.Jump { Context = Context };
                }

            }

            yield return new WorkItems.StopWalking { Context = Context };
        }
    }

    namespace WorkItems
    {
        /// <summary>
        /// Marker item to inform the runtime that the waypoint is being changed.
        /// </summary>
        public class WaypointChanged : WorkItem
        {
            public static WaypointChanged Singleton = new WaypointChanged();
            public override void Execute() { }
        }

        /// <summary>
        /// Faces the character and camera toward the destination waypoint and
        /// auto-runs toward it.
        /// </summary>
        public class WalkToWaypoint : WorkItem
        {
            public WalkpathNavigationContext Context { private get; set; }
            public Waypoint Destination { private get; set; }

            public bool ReachedDestination { get; private set; }

            public override void Execute()
            {
                Vector3 curLoc = Context.Eq2.Character.Location;

                // Check to see if we're at our destination.
                float distance = curLoc.DistanceTo(Destination.Vector, true);
                if (distance <= Context.WaypointPrecision)
                {
                    // We are at our destination. Set our output param and break out.
                    ReachedDestination = true;
                    return;
                }

                // Set us on the right heading.
                float newHeading = curLoc.HeadingTo(Destination.Vector);
                Context.Eq2.Character.Heading = newHeading;

                // Auto-run!
                Context.Eq2.Character.IsAutoRunning= true;
            }
        }

        /// <summary>
        /// Jumps the character.
        /// </summary>
        public class Jump : WorkItem
        {
            public WalkpathNavigationContext Context { private get; set; }

            public override void Execute()
            {
                Context.Keyboard.Press(Context.JumpKey);
            }
        }

        /// <summary>
        /// Stops the character from auto-running.
        /// </summary>
        public class StopWalking : WorkItem
        {
            public WalkpathNavigationContext Context { private get; set; }

            public override void Execute()
            {
                Context.Eq2.Character.IsAutoRunning= false;
            }
        }
    }

}
