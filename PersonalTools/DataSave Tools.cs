using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTools
{
    class DataSave_Tools
    {
        //public bool Save(object SaveItem)
        //{
        //    /// <summary>
        //    /// This is the general save function for all view models. Any generic model can be thrown into this function
        //    /// and it should save provided the name of the values match up with the database exactly.
        //    /// </summary>
        //    /// <param name="SaveItem"></param>
        //    /// <returns></returns>
        //    try
        //    {
        //        UsedProposal = new Proposal();
        //        int? PID = (int?)SaveItem.GetType().GetProperty("PID").GetValue(SaveItem, null);
        //        if (PID != null) //
        //        {
        //            UsedProposal = entitys.Proposals.AsNoTracking().Where(x => x.PID == PID.Value).SingleOrDefault();
        //        }
        //        else
        //        {
        //            UsedProposal = new Proposal();

        //            UsedProposal.DateProposalCreated = DateTime.Now;
        //            UsedProposal.ProposalRef = CreateQuoteReference();

        //            UsedProposal.ProposalStatus = "Created";
        //        }
        //        foreach (var prop in SaveItem.GetType().GetProperties())
        //        {
        //            var aValue = SaveItem.GetType().GetProperty(prop.Name).GetValue(SaveItem, null);
        //            string[] IgnoreValues = { "EmailAddress2", "Password", "ConfirmEmailAddress", "InsuranceRequired", "ActList", "TurnOverList", "TotalValofClaims", "TitleList", "CreateAndSaveQuote" };
        //            string[] Dates = { "PolStartDate", "PolEndDate" };
        //            if (!IgnoreValues.Contains(prop.Name))
        //            {
        //                System.Type typeCode = prop.PropertyType.GetType();

        //                try
        //                {
        //                    if (prop.Name == "ADDOccupationList")
        //                    {
        //                        if (prop.PropertyType.Name.Equals("String"))
        //                        {
        //                            UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, aValue);
        //                        }
        //                        else
        //                        {
        //                            UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, ConvertOccupation((int[])aValue));
        //                        }
        //                    }
        //                    //}
        //                    //else if (Dates.Contains(prop.Name)) // Is a date value
        //                    //{
        //                    //if (prop.PropertyType.Name.Equals("String"))
        //                    //{
        //                    //    DateTime DateToSave = DateTime.Parse((string)aValue, new System.Globalization.CultureInfo("en-GB", true), System.Globalization.DateTimeStyles.AssumeLocal);
        //                    //    UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, DateToSave);

        //                    //}
        //                    //else
        //                    //{
        //                    //    UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, aValue);
        //                    //}
        //                    // }
        //                    else
        //                    {
        //                        //This is a string type
        //                        if (prop.PropertyType.Name.Equals("String"))
        //                        {
        //                            UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, ToupperTolowerFordDatabase((string)aValue));
        //                        }
        //                        {
        //                            UsedProposal.GetType().GetProperty(prop.Name).SetValue(UsedProposal, aValue);
        //                        }
        //                    }
        //                }
        //                catch (System.Exception exc)
        //                {

        //                }
        //            }
        //        }
        //        if (SaveItem.GetType().GetProperty("PID").GetValue(SaveItem, null) == null) //
        //        {
        //            try
        //            {
        //                entitys.Set<Proposal>().Add(UsedProposal);
        //                entitys.SaveChanges();
        //                SaveItem.GetType().GetProperty("PID").SetValue(SaveItem, UsedProposal.PID);
        //                //  = ; //store new or current id
        //            }
        //            catch (DbEntityValidationException e)
        //            {
        //                string ErrorList = "";
        //                foreach (var eve in e.EntityValidationErrors)
        //                {
        //                    ErrorList = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name + eve.Entry.State.ToString();
        //                    foreach (var ve in eve.ValidationErrors)
        //                    {
        //                        ErrorList += "- Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName + ve.ErrorMessage.ToString();
        //                    }
        //                }
        //                throw;
        //            }

        //        }
        //        entitys.Entry(UsedProposal).State = System.Data.Entity.EntityState.Modified;
        //        entitys.SaveChanges();
        //        return true;
        //    }
        //    catch (System.Exception exc)
        //    {
        //        return false;
        //    }

        //}
    }
}
