using API_StaffTrack.Data.EF;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Models.Common;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Application.Services
{
    public interface IS_Employee
    {
        Task<ResponseData<MRes_Employee>> Create(MReq_Employee request);
        Task<ResponseData<MRes_Employee>> Update(int id,MReq_Employee request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_Employee>> GetById(int id);
        Task<ResponseData<List<MRes_Employee>>> GetAll();
        Task<ResponseData<List<MRes_Employee>>> GetListByStatus(int status);
    }
    public class S_Employee : IS_Employee
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_Employee(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_Employee>> Create(MReq_Employee request)
        {
            var res = new ResponseData<MRes_Employee>();
            try
            {
                var exists = await _context.Employees.AnyAsync(x => x.Email == request.Email && x.Status != -1);
                if (exists)
                {
                    res.error.message = "Email đã tồn tại!";
                    return res;
                }

                var employee = _mapper.Map<Employee>(request);
                employee.CreatedAt = DateTime.Now;
                employee.UpdateAt = DateTime.Now;
                employee.Status = request.Status ?? 1;
                employee.IsActive = request.IsActive ?? true;

                _context.Employees.Add(employee);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể tạo nhân viên.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Employee>(employee);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<MRes_Employee>> Update(int id, MReq_Employee request)
        {
            var res = new ResponseData<MRes_Employee>();
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    res.error.message = "Không tìm thấy nhân viên.";
                    return res;
                }

                _mapper.Map(request, employee);
                employee.UpdateAt = DateTime.Now;

                _context.Employees.Update(employee);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể cập nhật.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Employee>(employee);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<int>> Delete(int id)
        {
            var res = new ResponseData<int>();
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    res.error.message = "Không tìm thấy nhân viên.";
                    return res;
                }

                _context.Employees.Remove(employee);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể xóa.";
                    return res;
                }

                res.data = save;
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<MRes_Employee>> GetById(int id)
        {
            var res = new ResponseData<MRes_Employee>();
            try
            {
                var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (employee == null)
                {
                    res.error.message = "Không tìm thấy nhân viên.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Employee>(employee);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_Employee>>> GetAll()
        {
            var res = new ResponseData<List<MRes_Employee>>();
            try
            {
                var list = await _context.Employees.AsNoTracking().ToListAsync();
                res.data = _mapper.Map<List<MRes_Employee>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_Employee>>> GetListByStatus(int status)
        {
            var res = new ResponseData<List<MRes_Employee>>();
            try
            {
                var list = await _context.Employees.AsNoTracking().Where(x => x.Status == status).ToListAsync();
                res.data = _mapper.Map<List<MRes_Employee>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

       
    }

}
