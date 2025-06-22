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
    public interface IS_MonthlyReport
    {
        Task<ResponseData<MRes_MonthlyReport>> Create(MReq_MonthlyReport request);
        Task<ResponseData<MRes_MonthlyReport>> Update(int id, MReq_MonthlyReport request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_MonthlyReport>> GetById(int id);
        Task<ResponseData<List<MRes_MonthlyReport>>> GetAll();
        Task<ResponseData<List<MRes_MonthlyReport>>> GetByEmployee(int employeeId);
    }
    public class S_MonthlyReport : IS_MonthlyReport
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_MonthlyReport(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_MonthlyReport>> Create(MReq_MonthlyReport request)
        {
            var res = new ResponseData<MRes_MonthlyReport>();
            try
            {
                var entity = _mapper.Map<MonthlyReport>(request);
                entity.CreatedAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                entity.Status ??= 1;

                _context.MonthlyReports.Add(entity);
                await _context.SaveChangesAsync();

                res.data = _mapper.Map<MRes_MonthlyReport>(entity);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_MonthlyReport>> Update(int id, MReq_MonthlyReport request)
        {
            var res = new ResponseData<MRes_MonthlyReport>();
            try
            {
                var entity = await _context.MonthlyReports.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy báo cáo.";
                    return res;
                }

                _mapper.Map(request, entity);
                entity.UpdateAt = DateTime.Now;

                _context.MonthlyReports.Update(entity);
                await _context.SaveChangesAsync();

                res.data = _mapper.Map<MRes_MonthlyReport>(entity);
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
                var entity = await _context.MonthlyReports.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy báo cáo.";
                    return res;
                }

                _context.MonthlyReports.Remove(entity);
                res.data = await _context.SaveChangesAsync();
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_MonthlyReport>> GetById(int id)
        {
            var res = new ResponseData<MRes_MonthlyReport>();
            try
            {
                var entity = await _context.MonthlyReports.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy báo cáo.";
                    return res;
                }

                res.data = _mapper.Map<MRes_MonthlyReport>(entity);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<List<MRes_MonthlyReport>>> GetAll()
        {
            var res = new ResponseData<List<MRes_MonthlyReport>>();
            try
            {
                var list = await _context.MonthlyReports.AsNoTracking().ToListAsync();
                res.data = _mapper.Map<List<MRes_MonthlyReport>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<List<MRes_MonthlyReport>>> GetByEmployee(int employeeId)
        {
            var res = new ResponseData<List<MRes_MonthlyReport>>();
            try
            {
                var list = await _context.MonthlyReports
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                res.data = _mapper.Map<List<MRes_MonthlyReport>>(list);
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
