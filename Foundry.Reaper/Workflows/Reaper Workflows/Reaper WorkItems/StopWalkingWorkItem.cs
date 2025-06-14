using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Reaper.Workflows {
    public class StopWalkingWorkItem : ReaperConfigurableWorkItem {
        public override void Execute() {
            Configuration.Eq2PointerLibrary.CharacterIsAutoRunning = false;
        }
    }
}
