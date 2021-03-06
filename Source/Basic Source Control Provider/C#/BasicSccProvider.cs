/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using ErrorHandler = Microsoft.VisualStudio.ErrorHandler;
using MsVsShell = Microsoft.VisualStudio.Shell;

namespace Microsoft.Samples.VisualStudio.SourceControlIntegration.BasicSccProvider
{
    /////////////////////////////////////////////////////////////////////////////
    // BasicSccProvider
    [MsVsShell.DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\11.0Exp")]
    // Declare that resources for the package are to be found in the managed assembly resources, and not in a satellite dll
    [MsVsShell.PackageRegistration(UseManagedResourcesOnly = true)]
    // Register the resource ID of the CTMENU section (generated from compiling the VSCT file), so the IDE will know how to merge this package's menus with the rest of the IDE when "devenv /setup" is run
    // The menu resource ID needs to match the ResourceName number defined in the csproj project file in the VSCTCompile section
    // Everytime the version number changes VS will automatically update the menus on startup; if the version doesn't change, you will need to run manually "devenv /setup /rootsuffix:Exp" to see VSCT changes reflected in IDE
    [MsVsShell.ProvideMenuResource(1000, 1)]
    // Register a sample options page visible as Tools/Options/SourceControl/SampleOptionsPage when the provider is active
    [MsVsShell.ProvideOptionPageAttribute(typeof(SccProviderOptions), "Source Control", "Sample Options Page Basic Provider", 106, 107, false)]
    [ProvideToolsOptionsPageVisibility("Source Control", "Sample Options Page Basic Provider", "ADC98052-0000-41D1-A6C3-704E6C1A3DE2")]
    // Register a sample tool window visible only when the provider is active
    [MsVsShell.ProvideToolWindow(typeof(SccProviderToolWindow))]
    [MsVsShell.ProvideToolWindowVisibility(typeof(SccProviderToolWindow), "ADC98052-0000-41D1-A6C3-704E6C1A3DE2")]
    // Register the source control provider's service (implementing IVsScciProvider interface)
    [MsVsShell.ProvideService(typeof(SccProviderService), ServiceName = "Source Control Sample Basic Provider Service")]
    // Register the source control provider to be visible in Tools/Options/SourceControl/Plugin dropdown selector
    [ProvideSourceControlProvider("Managed Source Control Sample Basic Provider", "#100")]
    // Pre-load the package when the command UI context is asserted (the provider will be automatically loaded after restarting the shell if it was active last time the shell was shutdown)
    [MsVsShell.ProvideAutoLoad("ADC98052-0000-41D1-A6C3-704E6C1A3DE2")]
    // Declare the package guid
    [Guid("ADC98052-2000-41D1-A6C3-704E6C1A3DE2")]
    public class BasicSccProvider : MsVsShell.Package
    {
        private SccProviderService sccService = null;

        public BasicSccProvider()
        {
            Trace.WriteLine(String.Format(CultureInfo.CurrentUICulture, "Entering constructor for: {0}", this.ToString()));
        }

        /////////////////////////////////////////////////////////////////////////////
        // BasicSccProvider Package Implementation
        #region Package Members

        protected override void Initialize()
        {
            Trace.WriteLine(String.Format(CultureInfo.CurrentUICulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Proffer the source control service implemented by the provider
            sccService = new SccProviderService(this);
            ((IServiceContainer)this).AddService(typeof(SccProviderService), sccService, true);

            // Add our command handlers for menu (commands must exist in the .vsct file)
            MsVsShell.OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as MsVsShell.OleMenuCommandService;
            if (mcs != null)
            {
                CommandID cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdSccCommand);
                MenuCommand menuCmd = new MenuCommand(new EventHandler(OnSccCommand), cmd);
                mcs.AddCommand(menuCmd);

                // ToolWindow Command
                cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdViewToolWindow);
                menuCmd = new MenuCommand(new EventHandler(ViewToolWindow), cmd);
                mcs.AddCommand(menuCmd);

                // ToolWindow's ToolBar Command
                cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdToolWindowToolbarCommand);
                menuCmd = new MenuCommand(new EventHandler(ToolWindowToolbarCommand), cmd);
                mcs.AddCommand(menuCmd);
            }

            // Register the provider with the source control manager
            // If the package is to become active, this will also callback on OnActiveStateChange and the menu commands will be enabled
            IVsRegisterScciProvider rscp = (IVsRegisterScciProvider)GetService(typeof(IVsRegisterScciProvider));
            rscp.RegisterSourceControlProvider(GuidList.guidSccProvider);
        }

        protected override void Dispose(bool disposing)
        {
            Trace.WriteLine(String.Format(CultureInfo.CurrentUICulture, "Entering Dispose() of: {0}", this.ToString()));

            base.Dispose(disposing);
        }

        #endregion


        private void OnSccCommand(object sender, EventArgs e)
        {
            // Toggle the checked state of this command
            MenuCommand thisCommand = sender as MenuCommand;
            if (thisCommand != null)
            {
                thisCommand.Checked = !thisCommand.Checked;
            }
        }

        /// <summary>
        /// The function can be used to bring back the provider's toolwindow if it was previously closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewToolWindow(object sender, EventArgs e)
        {
            this.ShowSccProviderToolWindow();
        }

        private void ToolWindowToolbarCommand(object sender, EventArgs e)
        {
            SccProviderToolWindow window = (SccProviderToolWindow)this.FindToolWindow(typeof(SccProviderToolWindow), 0, true);

            if (window != null)
            {
                window.ToolWindowToolbarCommand();
            }
        }

        // This function is called by the IVsSccProvider service implementation when the active state of the provider changes
        // The package needs to show or hide the scc-specific commands 
        public virtual void OnActiveStateChange()
        {
            MsVsShell.OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as MsVsShell.OleMenuCommandService;
            if (mcs != null)
            {
                CommandID cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdSccCommand);
                MenuCommand menuCmd = mcs.FindCommand(cmd);
                menuCmd.Supported = true;
                menuCmd.Enabled = sccService.Active;
                menuCmd.Visible = sccService.Active;

                cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdViewToolWindow);
                menuCmd = mcs.FindCommand(cmd);
                menuCmd.Supported = true;
                menuCmd.Enabled = sccService.Active;
                menuCmd.Visible = sccService.Active;

                cmd = new CommandID(GuidList.guidSccProviderCmdSet, CommandId.icmdToolWindowToolbarCommand);
                menuCmd = mcs.FindCommand(cmd);
                menuCmd.Supported = true;
                menuCmd.Enabled = sccService.Active;
                menuCmd.Visible = sccService.Active;
            }

            ShowSccProviderToolWindow();
        }

        private void ShowSccProviderToolWindow()
        {
            MsVsShell.ToolWindowPane window = this.FindToolWindow(typeof(SccProviderToolWindow), 0, true);
            IVsWindowFrame windowFrame = null;
            if (window != null && window.Frame != null)
            {
                windowFrame = (IVsWindowFrame)window.Frame;
            }
            if (windowFrame == null)
            {
                throw new InvalidOperationException("No valid window frame object was returned from Toolwindow pane");
            }
            if (sccService.Active)
            {
                ErrorHandler.ThrowOnFailure(windowFrame.Show());
            }
            else
            {
                ErrorHandler.ThrowOnFailure(windowFrame.Hide());
            }
        }
    }
}