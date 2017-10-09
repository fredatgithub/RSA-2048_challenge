﻿#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using GetPrimeFiles.Properties;
using Ionic.Zip;

namespace GetPrimeFiles
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
    }

    public readonly Dictionary<string, string> _languageDicoEn = new Dictionary<string, string>();
    public readonly Dictionary<string, string> _languageDicoFr = new Dictionary<string, string>();
    private string _currentLanguage = "english";
    private ConfigurationOptions _configurationOptions = new ConfigurationOptions();

    private void QuitToolStripMenuItemClick(object sender, EventArgs e)
    {
      SaveWindowValue();
      Application.Exit();
    }

    private void AboutToolStripMenuItemClick(object sender, EventArgs e)
    {
      AboutBoxApplication aboutBoxApplication = new AboutBoxApplication();
      aboutBoxApplication.ShowDialog();
    }

    private void DisplayTitle()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      Text += string.Format(" V{0}.{1}.{2}.{3}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart,
        fvi.FilePrivatePart);
    }

    private void FormMainLoad(object sender, EventArgs e)
    {
      LoadSettingsAtStartup();
    }

    private void LoadSettingsAtStartup()
    {
      DisplayTitle();
      GetWindowValue();
      LoadLanguages();
      SetLanguage(Settings.Default.LastLanguageUsed);
    }

    private void LoadConfigurationOptions()
    {
      _configurationOptions.Option1Name = Settings.Default.Option1Name;
      _configurationOptions.Option2Name = Settings.Default.Option2Name;
    }

    private void SaveConfigurationOptions()
    {
      _configurationOptions.Option1Name = Settings.Default.Option1Name;
      _configurationOptions.Option2Name = Settings.Default.Option2Name;
    }

    private void LoadLanguages()
    {
      if (!File.Exists(Settings.Default.LanguageFileName))
      {
        CreateLanguageFile();
      }

      // read the translation file and feed the language
      XDocument xDoc;
      try
      {
        xDoc = XDocument.Load(Settings.Default.LanguageFileName);
      }
      catch (Exception exception)
      {
        MessageBox.Show(Resources.Error_while_loading_the + Punctuation.OneSpace +
          Settings.Default.LanguageFileName + Punctuation.OneSpace + Resources.XML_file +
          Punctuation.OneSpace + exception.Message);
        CreateLanguageFile();
        return;
      }
      var result = from node in xDoc.Descendants("term")
                   where node.HasElements
                   let xElementName = node.Element("name")
                   where xElementName != null
                   let xElementEnglish = node.Element("englishValue")
                   where xElementEnglish != null
                   let xElementFrench = node.Element("frenchValue")
                   where xElementFrench != null
                   select new
                   {
                     name = xElementName.Value,
                     englishValue = xElementEnglish.Value,
                     frenchValue = xElementFrench.Value
                   };
      foreach (var i in result)
      {
        if (!_languageDicoEn.ContainsKey(i.name))
        {
          _languageDicoEn.Add(i.name, i.englishValue);
        }
#if DEBUG
        else
        {
          MessageBox.Show(Resources.Your_XML_file_has_duplicate_like + Punctuation.Colon +
            Punctuation.OneSpace + i.name);
        }
#endif
        if (!_languageDicoFr.ContainsKey(i.name))
        {
          _languageDicoFr.Add(i.name, i.frenchValue);
        }
#if DEBUG
        else
        {
          MessageBox.Show(Resources.Your_XML_file_has_duplicate_like + Punctuation.Colon +
            Punctuation.OneSpace + i.name);
        }
#endif
      }
    }

    private static void CreateLanguageFile()
    {
      List<string> minimumVersion = new List<string>
      {
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
        "<terms>",
         "<term>",
        "<name>MenuFile</name>",
        "<englishValue>File</englishValue>",
        "<frenchValue>Fichier</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileNew</name>",
        "<englishValue>New</englishValue>",
        "<frenchValue>Nouveau</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileOpen</name>",
        "<englishValue>Open</englishValue>",
        "<frenchValue>Ouvrir</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileSave</name>",
        "<englishValue>Save</englishValue>",
        "<frenchValue>Enregistrer</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFileSaveAs</name>",
        "<englishValue>Save as ...</englishValue>",
        "<frenchValue>Enregistrer sous ...</frenchValue>",
        "</term>",
        "<term>",
        "<name>MenuFilePrint</name>",
        "<englishValue>Print ...</englishValue>",
        "<frenchValue>Imprimer ...</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenufilePageSetup</name>",
          "<englishValue>Page setup</englishValue>",
          "<frenchValue>Aperçu avant impression</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenufileQuit</name>",
          "<englishValue>Quit</englishValue>",
          "<frenchValue>Quitter</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEdit</name>",
          "<englishValue>Edit</englishValue>",
          "<frenchValue>Edition</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCancel</name>",
          "<englishValue>Cancel</englishValue>",
          "<frenchValue>Annuler</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditRedo</name>",
          "<englishValue>Redo</englishValue>",
          "<frenchValue>Rétablir</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCut</name>",
          "<englishValue>Cut</englishValue>",
          "<frenchValue>Couper</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditCopy</name>",
          "<englishValue>Copy</englishValue>",
          "<frenchValue>Copier</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditPaste</name>",
          "<englishValue>Paste</englishValue>",
          "<frenchValue>Coller</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuEditSelectAll</name>",
          "<englishValue>Select All</englishValue>",
          "<frenchValue>Sélectionner tout</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuTools</name>",
          "<englishValue>Tools</englishValue>",
          "<frenchValue>Outils</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuToolsCustomize</name>",
          "<englishValue>Customize ...</englishValue>",
          "<frenchValue>Personaliser ...</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuToolsOptions</name>",
          "<englishValue>Options</englishValue>",
          "<frenchValue>Options</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguage</name>",
          "<englishValue>Language</englishValue>",
          "<frenchValue>Langage</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguageEnglish</name>",
          "<englishValue>English</englishValue>",
          "<frenchValue>Anglais</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuLanguageFrench</name>",
          "<englishValue>French</englishValue>",
          "<frenchValue>Français</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelp</name>",
          "<englishValue>Help</englishValue>",
          "<frenchValue>Aide</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpSummary</name>",
          "<englishValue>Summary</englishValue>",
          "<frenchValue>Sommaire</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpIndex</name>",
          "<englishValue>Index</englishValue>",
          "<frenchValue>Index</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpSearch</name>",
          "<englishValue>Search</englishValue>",
          "<frenchValue>Rechercher</frenchValue>",
        "</term>",
        "<term>",
          "<name>MenuHelpAbout</name>",
          "<englishValue>About</englishValue>",
          "<frenchValue>A propos de ...</frenchValue>",
        "</term>",
        "</terms>"
      };
      StreamWriter sw = new StreamWriter(Settings.Default.LanguageFileName);
      foreach (string item in minimumVersion)
      {
        sw.WriteLine(item);
      }

      sw.Close();
    }

    private void GetWindowValue()
    {
      Width = Settings.Default.WindowWidth;
      Height = Settings.Default.WindowHeight;
      Top = Settings.Default.WindowTop < 0 ? 0 : Settings.Default.WindowTop;
      Left = Settings.Default.WindowLeft < 0 ? 0 : Settings.Default.WindowLeft;
      SetDisplayOption(Settings.Default.DisplayToolStripMenuItem);
      LoadConfigurationOptions();
    }

    private void SaveWindowValue()
    {
      Settings.Default.WindowHeight = Height;
      Settings.Default.WindowWidth = Width;
      Settings.Default.WindowLeft = Left;
      Settings.Default.WindowTop = Top;
      Settings.Default.LastLanguageUsed = frenchToolStripMenuItem.Checked ? "French" : "English";
      Settings.Default.DisplayToolStripMenuItem = GetDisplayOption();
      SaveConfigurationOptions();
      Settings.Default.Save();
    }

    private string GetDisplayOption()
    {
      if (SmallToolStripMenuItem.Checked)
      {
        return "Small";
      }

      if (MediumToolStripMenuItem.Checked)
      {
        return "Medium";
      }

      return LargeToolStripMenuItem.Checked ? "Large" : string.Empty;
    }

    private void SetDisplayOption(string option)
    {
      UncheckAllOptions();
      switch (option.ToLower())
      {
        case "small":
          SmallToolStripMenuItem.Checked = true;
          break;
        case "medium":
          MediumToolStripMenuItem.Checked = true;
          break;
        case "large":
          LargeToolStripMenuItem.Checked = true;
          break;
        default:
          SmallToolStripMenuItem.Checked = true;
          break;
      }
    }

    private void UncheckAllOptions()
    {
      SmallToolStripMenuItem.Checked = false;
      MediumToolStripMenuItem.Checked = false;
      LargeToolStripMenuItem.Checked = false;
    }

    private void FormMainFormClosing(object sender, FormClosingEventArgs e)
    {
      SaveWindowValue();
    }

    private void FrenchToolStripMenuItemClick(object sender, EventArgs e)
    {
      _currentLanguage = Language.French.ToString();
      SetLanguage(Language.French.ToString());
      AdjustAllControls();
    }

    private void EnglishToolStripMenuItemClick(object sender, EventArgs e)
    {
      _currentLanguage = Language.English.ToString();
      SetLanguage(Language.English.ToString());
      AdjustAllControls();
    }

    private void SetLanguage(string myLanguage)
    {
      switch (myLanguage)
      {
        case "English":
          frenchToolStripMenuItem.Checked = false;
          englishToolStripMenuItem.Checked = true;
          fileToolStripMenuItem.Text = _languageDicoEn["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoEn["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoEn["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoEn["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoEn["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoEn["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoEn["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoEn["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoEn["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoEn["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoEn["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoEn["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoEn["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoEn["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoEn["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoEn["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoEn["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoEn["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoEn["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoEn["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoEn["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoEn["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoEn["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoEn["MenuHelpAbout"];
          DisplayToolStripMenuItem.Text = _languageDicoEn["Display"];
          SmallToolStripMenuItem.Text = _languageDicoEn["Small"];
          MediumToolStripMenuItem.Text = _languageDicoEn["Medium"];
          LargeToolStripMenuItem.Text = _languageDicoEn["Large"];


          _currentLanguage = "English";
          break;
        case "French":
          frenchToolStripMenuItem.Checked = true;
          englishToolStripMenuItem.Checked = false;
          fileToolStripMenuItem.Text = _languageDicoFr["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoFr["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoFr["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoFr["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoFr["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoFr["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoFr["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoFr["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoFr["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoFr["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoFr["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoFr["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoFr["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoFr["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoFr["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoFr["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoFr["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoFr["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoFr["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoFr["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoFr["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoFr["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoFr["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoFr["MenuHelpAbout"];
          DisplayToolStripMenuItem.Text = _languageDicoFr["Display"];
          SmallToolStripMenuItem.Text = _languageDicoFr["Small"];
          MediumToolStripMenuItem.Text = _languageDicoFr["Medium"];
          LargeToolStripMenuItem.Text = _languageDicoFr["Large"];

          _currentLanguage = "French";
          break;
        default:
          SetLanguage("English");
          break;
      }
    }

    private void CutToolStripMenuItemClick(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control> { }); // add your controls in the List
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        CutToClipboard(tb);
      }
    }

    private void CopyToolStripMenuItemClick(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control> { }); // add your controls in the List
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        CopyToClipboard(tb);
      }
    }

    private void PasteToolStripMenuItemClick(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control> { }); // add your controls in the List
      var tb = focusedControl as TextBox;
      if (tb != null)
      {
        PasteFromClipboard(tb);
      }
    }

    private void SelectAllToolStripMenuItemClick(object sender, EventArgs e)
    {
      Control focusedControl = FindFocusedControl(new List<Control> { }); // add your controls in the List
      TextBox control = focusedControl as TextBox;
      if (control != null) control.SelectAll();
    }

    private void CutToClipboard(TextBoxBase tb, string errorMessage = "nothing")
    {
      if (tb != ActiveControl) return;
      if (tb.Text == string.Empty)
      {
        DisplayMessage(Translate("ThereIs") + Punctuation.OneSpace +
          Translate(errorMessage) + Punctuation.OneSpace +
          Translate("ToCut") + Punctuation.OneSpace, Translate(errorMessage),
          MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessage(Translate("NoTextHasBeenSelected"),
          Translate(errorMessage), MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
      tb.SelectedText = string.Empty;
    }

    private void CopyToClipboard(TextBoxBase tb, string message = "nothing")
    {
      if (tb != ActiveControl) return;
      if (tb.Text == string.Empty)
      {
        DisplayMessage(Translate("ThereIsNothingToCopy") + Punctuation.OneSpace,
          Translate(message), MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessage(Translate("NoTextHasBeenSelected"),
          Translate(message), MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
    }

    private void PasteFromClipboard(TextBoxBase tb)
    {
      if (tb != ActiveControl) return;
      var selectionIndex = tb.SelectionStart;
      tb.SelectedText = Clipboard.GetText();
      tb.SelectionStart = selectionIndex + Clipboard.GetText().Length;
    }

    private void DisplayMessage(string message, string title, MessageBoxButtons buttons)
    {
      MessageBox.Show(this, message, title, buttons);
    }

    private string Translate(string index)
    {
      string result = string.Empty;
      switch (_currentLanguage.ToLower())
      {
        case "english":
          result = _languageDicoEn.ContainsKey(index) ? _languageDicoEn[index] :
           "the term: \"" + index + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
        case "french":
          result = _languageDicoFr.ContainsKey(index) ? _languageDicoFr[index] :
            "the term: \"" + index + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
      }

      return result;
    }

    private static Control FindFocusedControl(Control container)
    {
      foreach (Control childControl in container.Controls.Cast<Control>().Where(childControl => childControl.Focused))
      {
        return childControl;
      }

      return (from Control childControl in container.Controls
              select FindFocusedControl(childControl)).FirstOrDefault(maybeFocusedControl => maybeFocusedControl != null);
    }

    private static Control FindFocusedControl(List<Control> container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    private static Control FindFocusedControl(params Control[] container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    private static Control FindFocusedControl(IEnumerable<Control> container)
    {
      return container.FirstOrDefault(control => control.Focused);
    }

    private static string PeekDirectory()
    {
      string result = string.Empty;
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      if (fbd.ShowDialog() == DialogResult.OK)
      {
        result = fbd.SelectedPath;
      }

      return result;
    }

    private string PeekFile()
    {
      string result = string.Empty;
      OpenFileDialog fd = new OpenFileDialog();
      if (fd.ShowDialog() == DialogResult.OK)
      {
        result = fd.SafeFileName;
      }

      return result;
    }

    private void SmallToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      SmallToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private void MediumToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      MediumToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private void LargeToolStripMenuItemClick(object sender, EventArgs e)
    {
      UncheckAllOptions();
      LargeToolStripMenuItem.Checked = true;
      AdjustAllControls();
    }

    private static void AdjustControls(params Control[] listOfControls)
    {
      if (listOfControls.Length == 0)
      {
        return;
      }

      int position = listOfControls[0].Width + 33; // 33 is the initial padding
      bool isFirstControl = true;
      foreach (Control control in listOfControls)
      {
        if (isFirstControl)
        {
          isFirstControl = false;
        }
        else
        {
          control.Left = position + 10;
          position += control.Width;
        }
      }
    }

    private void AdjustAllControls()
    {
      AdjustControls(); // insert here all labels, textboxes and buttons, one method per line of controls
    }

    private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
    {
      FormOptions frmOptions = new FormOptions(_configurationOptions);

      if (frmOptions.ShowDialog() == DialogResult.OK)
      {
        _configurationOptions = frmOptions.ConfigurationOptions2;
      }
    }

    private static void SetButtonEnabled(Button button, params Control[] controls)
    {
      bool result = true;
      foreach (Control ctrl in controls)
      {
        if (ctrl.GetType() == typeof(TextBox))
        {
          if (((TextBox)ctrl).Text == string.Empty)
          {
            result = false;
            break;
          }
        }

        if (ctrl.GetType() == typeof(ListView))
        {
          if (((ListView)ctrl).Items.Count == 0)
          {
            result = false;
            break;
          }
        }

        if (ctrl.GetType() == typeof(ComboBox))
        {
          if (((ComboBox)ctrl).SelectedIndex == -1)
          {
            result = false;
            break;
          }
        }
      }

      button.Enabled = result;
    }

    private void TextBoxNameKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        // do something
      }
    }

    private void buttonGetFiles_Click(object sender, EventArgs e)
    {
      // get prime files
      // get https://primes.utm.edu/lists/small/millions/primes1.zip
      //up to
      //https://primes.utm.edu/lists/small/millions/primes50.zip
      //download and unzip
      string extensionName = ".zip";
      string fileNameNumbered = "primes";
      // for loop to insert here
      int i = 1;
      string finalFileName = $"{fileNameNumbered}{i}{extensionName}";
      string url = $"https://primes.utm.edu/lists/small/millions/{finalFileName}";
      //test if file doesn't already exist
      if (!File.Exists(finalFileName))
      {
        bool result = GetWebClientBinaries(url, finalFileName);
        string messageToDisplay = $"The file has {NegateIfNeeded(result)}been downloaded correctly";
        string titleToDisplay = $"Download {NegateIfNeeded(result)}ok";
        DisplayMessage(messageToDisplay, titleToDisplay, MessageBoxButtons.OK);
      }
      
      //unziping
      if (File.Exists(finalFileName))
      {
        bool resultUnzip = UnzipFile(finalFileName, new DirectoryInfo(finalFileName));
      }
      
    }

    private static bool UnzipFile(string zipFileName, DirectoryInfo directory)
    {
      //Try
      //  Using zip As ZipFile = ZipFile.Read(zipToRead)
      //totalEntriesToProcess = zip.Entries.Count
      //SetProgressBarMax(zip.Entries.Count)
      //AddHandler zip.ExtractProgress, New EventHandler(Of ExtractProgressEventArgs)(AddressOf Me.zip_ExtractProgress)
      //zip.ExtractAll(extractDir, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
      //End Using
      //Catch ex1 As Exception
      //MessageBox.Show(String.Format("There's been a problem extracting that zip file.  {0}", ex1.Message), "Error Extracting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
      //End Try
      bool result = false;
      try
      {
        string zipToUnpack = "C1P3SML.zip";
        string unpackDirectory = "Extracted Files";
        using (ZipFile zip1 = ZipFile.Read(zipFileName))
        {
          // here, we extract every entry, but we could extract conditionally
          // based on entry name, size, date, checkbox status, etc.  
          foreach (ZipEntry file in zip1)
          {
            file.Extract(directory.Name, ExtractExistingFileAction.OverwriteSilently);
          }
        }

        result = true;
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
        return false;
      }

      return result;
    }

    private static string NegateIfNeeded(bool negativeValue)
    {
      return negativeValue ? string.Empty : $"not ";
    }

    private static bool GetWebClientBinaries(string url = "http://www.google.com/",
      string fileName = "untitled-file.pdf")
    {
      WebClient client = new WebClient();
      bool result = false;
      // set the user agent to IE6
      client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
      try
      {
        client.DownloadFile(url, fileName);
        result = true;
      }
      catch (WebException we)
      {
        //Console.WriteLine(we.Message + "\n" + we.Status);
        result = false;
      }
      catch (NotSupportedException ne)
      {
        //Console.WriteLine(ne.Message);
        result = false;
      }

      return result;
    }

    private static bool IsInternetConnected()
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.google.fr");

      try
      {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}