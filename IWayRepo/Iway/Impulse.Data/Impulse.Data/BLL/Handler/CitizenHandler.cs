using Impulse.Model;
using db = Impulse.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.Data.BLL.Interface;
using Impulse.Data.BLL.EF;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Data.BLL.Handler
{
    public class CitizenHandler : ICitizen
    {
        private readonly DataContext _context;

        public CitizenHandler(DataContext context)
        {
            _context = context;
        }
       
        public async Task<bool> SaveCitizenDetails(Citizen itm)
        {
            try
            {

                if (itm != null)
                {

                    var item = await _context.Citizen.Where(i => i.UserId == itm.UserId).FirstOrDefaultAsync();
                    if (item == null)
                    {
                        db.Citizen _citizen = new db.Citizen();
                        _citizen.UserId = itm.UserId;
                        _citizen.CaptainName = itm.CaptainName;
                        _citizen.PlayerName = itm.PlayerName;
                        _citizen.CompanyName = itm.CompanyName;
                        _citizen.Designation = itm.Designation;
                        _citizen.DOB = itm.DOB;

                        _citizen.BloodGroup = itm.BloodGroup;
                        _citizen.HREmail = itm.HREmail;
                        _citizen.HRPhone = itm.HRPhone;
                        _citizen.CompanyEmail = itm.CompanyEmail;

                        _context.Citizen.Add(_citizen);
                        _context.SaveChanges();
                    }
                    else
                    {

                        item.CaptainName = itm.CaptainName;
                        item.PlayerName = itm.PlayerName;
                        item.CompanyName = itm.CompanyName;
                        item.Designation = itm.Designation;
                        item.DOB = itm.DOB;
                        item.BloodGroup = itm.BloodGroup;
                        item.HREmail = itm.HREmail;
                        item.CompanyEmail = itm.CompanyEmail;
                        item.HRPhone = itm.HRPhone;
                        _context.SaveChanges();
                    }

                }

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Citizen> getCitizenDetails(int UserId)
        {
            try
            {
                Citizen _citizen = new Citizen();
                var itm = await _context.Citizen.Where(i => i.UserId == UserId).FirstOrDefaultAsync();
                if (itm != null)
                {

                    _citizen.UserId = itm.UserId;
                    _citizen.CitizenId = itm.CitizenId;
                    _citizen.CaptainName = itm.CaptainName;
                    _citizen.PlayerName = itm.PlayerName;
                    _citizen.CompanyName = itm.CompanyName;
                    _citizen.Designation = itm.Designation;
                    _citizen.DOB = itm.DOB;

                    _citizen.BloodGroup = itm.BloodGroup;
                    _citizen.HREmail = itm.HREmail;
                    _citizen.HRPhone = itm.HRPhone;
                    _citizen.CompanyEmail = itm.CompanyEmail;
                }



                return await Task.Run(() => _citizen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
