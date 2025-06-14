using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;
using Foundry.Autocrat.Geometry;

namespace Foundry.Reaper.Workflows {

	// Lasciate ogni speranza voi ch'entrate...
	public class ReaperWorkflow : ReaperConfigurableWorkflow {

		public ReaperWorkflow(ReaperConfiguration configuration) : base(configuration) { }

		protected override IEnumerable<WorkItem> WorkFlow() {
			if (Configuration == null) yield break;

			var target = new TargetWorkItem();
			var delayCheck = new DelayCheckWorkItem();

			for (int i = 0; i < Configuration.NumberOfRuns; i++) {
				var navWorkflow = new NavigationWorkflow(Configuration);

				// Stuck Workflow Handling
				// if (stuckWorkflow.WasStuck) navWorkflow.NavigationInterrupted = true;
				// if (stuckWorkflow.HopelesslyStuck) yield break;

				foreach (var navItem in navWorkflow) {
					yield return delayCheck;
					yield return navItem;

					if (!Configuration.Delaying) {
						yield return target;

						if (target.HasTarget) {
							var diat = new DetermineIsActionableTarget
							{
								SpawnId = target.TargetSpawnId,
								Location = target.TargetLocation,
								Name = target.TargetName
							};
							yield return diat;

							bool harvestable = diat.IsHarvestable;
							bool huntable = diat.IsHuntable;

							if (harvestable || huntable) {

                                Console.WriteLine("Is Harvestable!");

								var destination = target.TargetLocation;

								WalkWorkflow walkWorkflow = new WalkWorkflow(Configuration) { Destination = destination };

								var playerTarget = new TargetWorkItem();
								bool playerDetected = false;

								foreach (var walkItem in walkWorkflow) {
									// Stuck Workflow Handling
									// if (stuckWorkflow.WasStuck) walkWorkflow.NavigationInterrupted = true;
									// if (stuckWorkflow.HopelesslyStuck) yield break;

									// Ensure our node or mob isn't being taken by another player.
									// Note: this only works on non-pvp servers; pvp servers don't have a player-select key
									if (Configuration.DoPlayerDetection) {
										yield return playerTarget;

										if (playerTarget.HasTarget) {
											var playerDetectWorkItem = new PlayerDetectWorkItem
											{
												Destination = destination,
												PlayerLocation = playerTarget.TargetLocation,
											};
											yield return playerDetectWorkItem;

											if (playerDetectWorkItem.PlayerTooClose) {
												// Another player is too close to our target for comfort.
												playerDetected = true;
												break;
											}
										}
									}

									yield return walkItem;
								}

                                yield return new StopWalkingWorkItem();

								bool troubleWithTarget = false;

								if (!playerDetected /* && walkWorkflow.ReachedDestination */) {
									// Now we need to ensure we've got the right spawn ID targetted
									var retarget = new RetargetSpawnIDWorkItem
									{
										TargetSpawnId = target.TargetSpawnId
									};

									for (int j = 0; j < Configuration.MaxTargetReacquisitionAttempts; j++) {
										yield return retarget;
										if (retarget.ReacquiredTarget) break;
									}

									if (retarget.ReacquiredTarget) {
										// Now we utilize our target.
										Workflow hWorkflow = null;
										if (harvestable) hWorkflow = new HarvestWorkflow(Configuration)
										{
											NodeSpawnId = target.TargetSpawnId
										};

										if (huntable) hWorkflow = null;// TODO: Create Hunter Workflow Here

										foreach (var hItem in hWorkflow) yield return hItem;

										//troubleWithTarget = !((ISuccessfulWorkflow)hWorkflow).Successful;
										var harvw = hWorkflow as HarvestWorkflow;
										//var huntw = hWorkflow as HunterWorkflow;
										
										if (harvw != null) troubleWithTarget = !harvw.Successful;
										//else if (huntw != null) troubleWithTarget = !huntw.Successful;
									} else {
										troubleWithTarget = true;
									}
								} else {
									troubleWithTarget = true;
								}

								if (troubleWithTarget) {
									// Ignore the node for 5 minutes by default.
									Configuration.IgnoredSpawns.Ignore(target.TargetSpawnId, TimeSpan.FromMinutes(5));
                                    Console.WriteLine("***Trouble with target...***");
								}

								if (playerDetected) {
									// Ignore the node for 1 minute by default.
									Configuration.IgnoredSpawns.Ignore(target.TargetSpawnId, TimeSpan.FromMinutes(1));
                                    Console.WriteLine("***Player Detected...***");
								}

								target = new TargetWorkItem();

								// Navigation was interrupted by the WalkWorkflow, so we need to let our
								// navigation workflow know. It should include logic that gets itself
								// back on track after an interruption.
								navWorkflow.NavigationInterrupted = true;
							}
						}
					}
				}
			}

			yield break;
		}

	}
}
