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
    public interface IS_AttendanceRecord
    {
        Task<ResponseData<MRes_AttendanceRecord>> Create(MReq_AttendanceRecord request);
        Task<ResponseData<MRes_AttendanceRecord>> Update(int id, MReq_AttendanceRecord request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_AttendanceRecord>> GetById(int id);
        Task<ResponseData<List<MRes_AttendanceRecord>>> GetAll();
        Task<ResponseData<List<MRes_AttendanceRecord>>> GetListByEmployee(int employeeId);
    }
    public class S_AttendanceRecord : IS_AttendanceRecord
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_AttendanceRecord(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_AttendanceRecord>> Create(MReq_AttendanceRecord request)
        {
            var res = new ResponseData<MRes_AttendanceRecord>();
            try
            {
                var attendanceDate = DateOnly.FromDateTime(request.AttendanceDate);

                var exists = await _context.AttendanceRecords
                    .AnyAsync(x => x.EmployeeId == request.EmployeeId && x.AttendanceDate == attendanceDate);

                if (exists)
                {
                    res.error.message = "Nhân viên đã điểm danh ngày này.";
                    return res;
                }

                var entity = _mapper.Map<AttendanceRecord>(request);
                entity.CreatedAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                entity.Status ??= 1;

                _context.AttendanceRecords.Add(entity);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể tạo bản ghi.";
                    return res;
                }

                var response = _mapper.Map<MRes_AttendanceRecord>(entity);
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

        public async Task<ResponseData<MRes_AttendanceRecord>> Update(int id, MReq_AttendanceRecord request)
        {
            var res = new ResponseData<MRes_AttendanceRecord>();
            try
            {
                var entity = await _context.AttendanceRecords.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy bản ghi.";
                    return res;
                }

                _mapper.Map(request, entity);
                entity.UpdateAt = DateTime.Now;

                _context.AttendanceRecords.Update(entity);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể cập nhật.";
                    return res;
                }

                var response = _mapper.Map<MRes_AttendanceRecord>(entity);
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
                var entity = await _context.AttendanceRecords.FindAsync(id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy bản ghi.";
                    return res;
                }

                _context.AttendanceRecords.Remove(entity);
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

        public async Task<ResponseData<MRes_AttendanceRecord>> GetById(int id)
        {
            var res = new ResponseData<MRes_AttendanceRecord>();
            try
            {
                var entity = await _context.AttendanceRecords.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    res.error.message = "Không tìm thấy bản ghi.";
                    return res;
                }

                var response = _mapper.Map<MRes_AttendanceRecord>(entity);
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

        public async Task<ResponseData<List<MRes_AttendanceRecord>>> GetAll()
        {
            var res = new ResponseData<List<MRes_AttendanceRecord>>();
            try
            {
                var list = await _context.AttendanceRecords.AsNoTracking().ToListAsync();
                var result = _mapper.Map<List<MRes_AttendanceRecord>>(list);
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

        public async Task<ResponseData<List<MRes_AttendanceRecord>>> GetListByEmployee(int employeeId)
        {
            var res = new ResponseData<List<MRes_AttendanceRecord>>();
            try
            {
                var list = await _context.AttendanceRecords
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                var result = _mapper.Map<List<MRes_AttendanceRecord>>(list);
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
