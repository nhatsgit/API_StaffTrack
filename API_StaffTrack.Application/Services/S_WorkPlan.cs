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
    public interface IS_WorkPlan
    {
        Task<ResponseData<MRes_WorkPlan>> Create(MReq_WorkPlan request);
        Task<ResponseData<MRes_WorkPlan>> Update(int id, MReq_WorkPlan request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_WorkPlan>> GetById(int id);
        Task<ResponseData<List<MRes_WorkPlan>>> GetAll();
        Task<ResponseData<List<MRes_WorkPlan>>> GetListByEmployee(int employeeId);
    }
public class S_WorkPlan : IS_WorkPlan
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_WorkPlan(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_WorkPlan>> Create(MReq_WorkPlan request)
        {
            var res = new ResponseData<MRes_WorkPlan>();
            try
            {
                var exists = await _context.WorkPlans
                    .AnyAsync(x => x.EmployeeId == request.EmployeeId);
                if (exists)
                {
                    res.error.message = "Kế hoạch cho nhân viên vào ngày này đã tồn tại.";
                    return res;
                }

                var entity = _mapper.Map<WorkPlan>(request);
                entity.CreatedAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                entity.Status ??= 1;

                _context.WorkPlans.Add(entity);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể tạo kế hoạch.";
                    return res;
                }

                var response = _mapper.Map<MRes_WorkPlan>(entity);
                response.EmployeeName = (await _context.Employees.FindAsync(entity.EmployeeId))?.Name ?? "";
                res.data = response;
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<MRes_WorkPlan>> Update(int id, MReq_WorkPlan request)
        {
            var res = new ResponseData<MRes_WorkPlan>();
            try
            {
                var entity = await _context.WorkPlans.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy kế hoạch.";
                    return res;
                }

                _mapper.Map(request, entity);
                entity.UpdateAt = DateTime.Now;

                _context.WorkPlans.Update(entity);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể cập nhật.";
                    return res;
                }

                var response = _mapper.Map<MRes_WorkPlan>(entity);
                response.EmployeeName = (await _context.Employees.FindAsync(entity.EmployeeId))?.Name ?? "";
                res.data = response;
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
                var entity = await _context.WorkPlans.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy kế hoạch.";
                    return res;
                }

                _context.WorkPlans.Remove(entity);
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

        public async Task<ResponseData<MRes_WorkPlan>> GetById(int id)
        {
            var res = new ResponseData<MRes_WorkPlan>();
            try
            {
                var entity = await _context.WorkPlans
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    res.error.message = "Không tìm thấy kế hoạch.";
                    return res;
                }

                var response = _mapper.Map<MRes_WorkPlan>(entity);
                response.EmployeeName = (await _context.Employees.FindAsync(entity.EmployeeId))?.Name ?? "";
                res.data = response;
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_WorkPlan>>> GetAll()
        {
            var res = new ResponseData<List<MRes_WorkPlan>>();
            try
            {
                var list = await _context.WorkPlans
                    .AsNoTracking()
                    .ToListAsync();

                var result = _mapper.Map<List<MRes_WorkPlan>>(list);
                foreach (var item in result)
                {
                    item.EmployeeName = (await _context.Employees.FindAsync(item.EmployeeId))?.Name ?? "";
                }

                res.data = result;
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_WorkPlan>>> GetListByEmployee(int employeeId)
        {
            var res = new ResponseData<List<MRes_WorkPlan>>();
            try
            {
                var list = await _context.WorkPlans
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                var result = _mapper.Map<List<MRes_WorkPlan>>(list);
                foreach (var item in result)
                {
                    item.EmployeeName = (await _context.Employees.FindAsync(item.EmployeeId))?.Name ?? "";
                }

                res.data = result;
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
