using Starcounter;

namespace OrganizationManagementApp
{
    partial class EmployeeProfile : Json
    {
        public string Address => Street + ", " + Zip + " " + City + ", " + Country;

        void Handle(Input.UpdateProfileTrigger update)
        {
            Transaction.Commit();
        }
    }
}
