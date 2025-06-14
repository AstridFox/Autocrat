using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Extensions.Events {
    public static class EventExtensions {
        public static void SafeEvent(this EventHandler evt, object sender, EventArgs e) {
            if (evt != null) evt(sender, e);
        }

        public static void SafeEvent<T>(this EventHandler<T> evt, object sender, T e) where T : EventArgs {
            if (evt != null) evt(sender, e);
        }
    }
}
