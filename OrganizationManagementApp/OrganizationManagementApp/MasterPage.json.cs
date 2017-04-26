using OrganizationManagementApp;
using Starcounter;

partial class MasterPage : Page {
    public void RefreshList() {
        CompanyList recentInvoices = (CompanyList)RecentInvoices;
        recentInvoices.Companies = Db.SQL("SELECT i FROM Company i");
    }
}
