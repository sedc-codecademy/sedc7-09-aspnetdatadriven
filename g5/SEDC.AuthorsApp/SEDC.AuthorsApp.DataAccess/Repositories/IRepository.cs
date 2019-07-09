using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.DataAccess
{
    public interface IRepository<T>
    {
        #region Methods

        // We use IList because it is more flexible than list. If we put list the method will just accept a List return type
        // IList on the other hand is implemented by many types and you can use all those types as a return type, no problem
        // That includes ArrayList, List, CollectionBase, Sortedlist, StringCollection and all other classes that implement these 
        IList<T> GetAll(string nameFragment);
        T GetById(int id);
        int Insert(T entity);

        #endregion

    }
}
