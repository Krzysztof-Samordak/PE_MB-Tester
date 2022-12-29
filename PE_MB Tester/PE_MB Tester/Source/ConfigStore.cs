/******************************************************
 *       Copyright Keysight Technologies 2021
 ******************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using MindWorks.Nimbus;
using Ivi.Driver;



// This file is used to customize how the driver installer registers the IVI.NET driver in the IVI Configuration Store.
// Several attributes can be used to specify additional entires that should be placed in the end user's IVI Configuration Store
// at driver installation time.

// Use the LogicalName attribute to have your driver installer create a LogicalName entry in the IVI Configuration Store.
// This attribute can be applied multiple times to install multiple logical names.
//
//	[assembly: LogicalName("TODO:LogicalName", "TODO:DriverSessionName")]
//

// Use the DriverSession attribute to have your driver installer create a DriverSession entry in the IVI Configuration Store.
// This attribute can be applied multiple times to install multiple driver sessions.
//
//	[assembly: DriverSession("TODO:DriverSessionName",
//		Cache = false,
//		DriverSetup = "TODO",
//		HardwareAsset = "TODO:HardwareAssetName",
//		InterchangeCheck = false,
//		QueryInstrumentStatus = true,
//		RangeCheck = false,
//		RecordCoercions = false,
//		Simulate = false)]
//

// Use the HardwareAsset attribute to have your driver installer create a HardwareAsset entry in the IVI Configuration Store.
// This attribute can be applied multiple times to install multiple hardware assets.
//
//	[assembly: HardwareAsset("TODO:HardwareAssetName", "TODO:ResourceDescriptor")]
//

// Use the ConfigurableInitialSetting attribute to have your driver installer create a ConfigurableInitialSetting entry in the IVI Configuration Store.
// This attribute can be applied multiple times to install multiple ConfigurableInitialSetting entries.
//
//	[assembly: ConfigurableInitialSetting("TODO:KeyName", 0 /*TODO:Value*/, UsedInSession.Required, "TODO:Description")]
//
