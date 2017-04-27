using Starcounter;

namespace OrganizationManagementApp
{
    partial class CompanyList : Page
    {
        public void RefreshData()
        {
            Companies = Db.SQL("SELECT i FROM Company i");
        }

        [CompanyList_json.Companies]
        partial class InvoicesItemPage
        {
            protected override void OnData()
            {
                base.OnData();
                this.Url = string.Format("/companies/{0}", this.CompanyNo);
            }
        }
    }
}
