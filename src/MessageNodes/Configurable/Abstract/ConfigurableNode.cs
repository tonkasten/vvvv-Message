﻿using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Packs.Messaging.Nodes
{
    public abstract class ConfigurableNode :  IPluginEvaluate, IPartImportsSatisfiedNotification
    {
        [Config("Configuration", DefaultString = "string Foo")]
        public IDiffSpread<string> FConfig;

        [Import()]
        protected ILogger FLogger;


        public virtual void OnImportsSatisfied()
        {
            FConfig.Changed += HandleConfigChange;
        }



        protected abstract void HandleConfigChange(IDiffSpread<string> configSpread);
        public abstract void Evaluate(int SpreadMax);
    }
}
