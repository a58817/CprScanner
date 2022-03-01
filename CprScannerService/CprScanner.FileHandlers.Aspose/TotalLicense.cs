namespace CprScanner.FileHandlers.Aspose
{
    public class TotalLicense
    {
	    private readonly global::Aspose.Email.License emailLicense;

	    private readonly global::Aspose.Pdf.License pdfLicense;

	    private readonly global::Aspose.Words.License wordsLicense;

        public TotalLicense()
        {
            this.emailLicense = new global::Aspose.Email.License();
            this.pdfLicense = new global::Aspose.Pdf.License();
            this.wordsLicense = new global::Aspose.Words.License();
        }

        public void SetLicense(string licenseFile)
        {
            this.emailLicense.SetLicense(licenseFile);
            this.pdfLicense.SetLicense(licenseFile);
            this.wordsLicense.SetLicense(licenseFile);
        }
    }
}
