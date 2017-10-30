using EzPay.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EzPay.Services
{
    public interface ICitizen
    {
        IEnumerable<Citizen> GetAll();
        Citizen GetById(long id);
        void Add(Citizen newCitizen);

        string GetFirstName(long id);
        string GetLastName(long id);

        Bill GetBills(long id);
    }
}
