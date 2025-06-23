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
    public interface IS_LeaveRequest
    {
        Task<ResponseData<MRes_LeaveRequest>> Create(MReq_LeaveRequest request);
        Task<ResponseData<MRes_LeaveRequest>> Update(int id, MReq_LeaveRequest request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_LeaveRequest>> GetById(int id);
        Task<ResponseData<List<MRes_LeaveRequest>>> GetAll();
        Task<ResponseData<List<MRes_LeaveRequest>>> GetByEmployee(int employeeId);
        Task<ResponseData<MRes_LeaveRequest>> Approve(int id, int approvedBy);
    }
    public class S_LeaveRequest : IS_LeaveRequest
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_LeaveRequest(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_LeaveRequest>> Create(MReq_LeaveRequest request)
        {
            var res = new ResponseData<MRes_LeaveRequest>();
            try
            {
                var entity = _mapper.Map<LeaveRequest>(request);
                entity.RequestDate = DateTime.Now;
                entity.CreatedAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                entity.Status ??= 1;

                _context.LeaveRequests.Add(entity);
                await _context.SaveChangesAsync();

                res.data = _mapper.Map<MRes_LeaveRequest>(entity);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_LeaveRequest>> Update(int id, MReq_LeaveRequest request)
        {
            var res = new ResponseData<MRes_LeaveRequest>();
            try
            {
                var entity = await _context.LeaveRequests.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy yêu cầu nghỉ phép.";
                    return res;
                }

                _mapper.Map(request, entity);
                entity.UpdateAt = DateTime.Now;

                _context.LeaveRequests.Update(entity);
                await _context.SaveChangesAsync();

                res.data = _mapper.Map<MRes_LeaveRequest>(entity);
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
                var entity = await _context.LeaveRequests.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy yêu cầu nghỉ phép.";
                    return res;
                }

                _context.LeaveRequests.Remove(entity);
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

        public async Task<ResponseData<MRes_LeaveRequest>> GetById(int id)
        {
            var res = new ResponseData<MRes_LeaveRequest>();
            try
            {
                var entity = await _context.LeaveRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy yêu cầu nghỉ phép.";
                    return res;
                }

                res.data = _mapper.Map<MRes_LeaveRequest>(entity);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<List<MRes_LeaveRequest>>> GetAll()
        {
            var res = new ResponseData<List<MRes_LeaveRequest>>();
            try
            {
                var list = await _context.LeaveRequests.AsNoTracking().ToListAsync();
                res.data = _mapper.Map<List<MRes_LeaveRequest>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }

        public async Task<ResponseData<List<MRes_LeaveRequest>>> GetByEmployee(int employeeId)
        {
            var res = new ResponseData<List<MRes_LeaveRequest>>();
            try
            {
                var list = await _context.LeaveRequests
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                res.data = _mapper.Map<List<MRes_LeaveRequest>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }
            return res;
        }
        public async Task<ResponseData<MRes_LeaveRequest>> Approve(int id, int approvedBy)
        {
            var res = new ResponseData<MRes_LeaveRequest>();
            try
            {
                var entity = await _context.LeaveRequests.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy yêu cầu nghỉ phép.";
                    return res;
                }

                entity.ApprovedBy = approvedBy;
                entity.UpdateAt = DateTime.Now;

                _context.LeaveRequests.Update(entity);
                await _context.SaveChangesAsync();

                res.data = _mapper.Map<MRes_LeaveRequest>(entity);
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
