using Starcounter;

namespace OrganizationManagementApp.view_model
{
    partial class RegisterSale : Json, IBound<Sales>
    {
        public Person person => Db.SQL<Person>("SELECT p FROM PERSON p where p.PersonNo=?", PersonNo).First;

        void Handle(Input.RegisterSaleTrigger save)
        {
            Transaction.Commit();
            Data = new Sales
            {
                Saler = person
            };
        }
    }
}
