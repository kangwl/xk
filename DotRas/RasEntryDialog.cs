﻿//--------------------------------------------------------------------------
// <copyright file="RasEntryDialog.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using DotRas.Design;
    using DotRas.Internal;
    using DotRas.Properties;

    /// <summary>
    /// Prompts the user to create or modify a phone book entry. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to modify an existing entry using the <b>RasEntryDialog</b> component.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasEntryDialog dialog = new RasEntryDialog())
    /// {
    ///     dialog.EntryName = "VPN Connection";
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
    ///     dialog.Style = RasDialogStyle.Edit;
    ///     if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    ///     {
    ///         ' The entry was modified successfully.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim dialog As RasEntryDialog
    /// Try
    ///     dialog = New RasEntryDialog
    ///     dialog.EntryName = "VPN Connection"
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)
    ///     dialog.Style = RasDialogStyle.Edit
    ///     If (dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
    ///         ' The entry was modified successfully.
    ///     End If
    /// Finally
    ///     If (dialog IsNot Nothing) Then
    ///         dialog.Dispose()
    ///     End If
    /// End Try
    /// ]]>
    /// </code>
    /// </example>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(RasEntryDialog), "DotRas.RasEntryDialog.bmp")]
    public sealed class RasEntryDialog : RasCommonDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEntryDialog"/> class.
        /// </summary>
        public RasEntryDialog()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the entry.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDEntryNameDesc")]
        public string EntryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether entries cannot be renamed while in edit mode.
        /// </summary>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("REDNoRenameDesc")]
        public bool NoRename
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style of dialog box to display.
        /// </summary>
        [DefaultValue(typeof(RasDialogStyle), "Create")]
        [SRCategory("CatBehavior")]
        [SRDescription("REDRasDialogStyleDesc")]
        public RasDialogStyle Style
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the full path (including filename) to the phone book.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDPhoneBookPathDesc")]
        public string PhoneBookPath
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all <see cref="RasEntryDialog"/> properties to their default values.
        /// </summary>
        public override void Reset()
        {
            this.EntryName = null;
            this.NoRename = false;
            this.Style = RasDialogStyle.Create;
            this.PhoneBookPath = null;

            base.Reset();
        }

        /// <summary>
        /// Overridden. Displays the modal dialog.
        /// </summary>
        /// <param name="hwndOwner">The handle of the window that owns the dialog box.</param>
        /// <returns><b>true</b> if the user completed the entry successfully, otherwise <b>false</b>.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            if (string.IsNullOrEmpty(this.PhoneBookPath))
            {
                ThrowHelper.ThrowArgumentException("PhoneBookPath", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            NativeMethods.RASENTRYDLG dlg = new NativeMethods.RASENTRYDLG();
            dlg.size = Marshal.SizeOf(typeof(NativeMethods.RASENTRYDLG));
            dlg.hwndOwner = hwndOwner;

            switch (this.Style)
            {
                case RasDialogStyle.Edit:
                    if (this.NoRename)
                    {
                        dlg.flags |= NativeMethods.RASEDFLAG.NoRename;
                    }

                    break;

                default:
                    dlg.flags |= NativeMethods.RASEDFLAG.NewEntry;
                    break;
            }

            if (this.Location != Point.Empty)
            {
                dlg.left = this.Location.X;
                dlg.top = this.Location.Y;

                dlg.flags |= NativeMethods.RASEDFLAG.PositionDlg;
            }

            bool retval = false;

            try
            {
                string entryName = null;
                if (!string.IsNullOrEmpty(this.EntryName))
                {
                    entryName = this.EntryName;
                }

                retval = UnsafeNativeMethods.Instance.EntryDlg(this.PhoneBookPath, entryName, ref dlg);
                if (retval)
                {
                    this.EntryName = dlg.entryName;
                }
            }
            catch (EntryPointNotFoundException)
            {
                ThrowHelper.ThrowNotSupportedException(Resources.Exception_NotSupportedOnPlatform);
            }

            return retval;
        }

        #endregion
    }
}