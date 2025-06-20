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
    public interface IS_Notification
    {
        Task<ResponseData<MRes_Notification>> Create(MReq_Notification request);
        Task<ResponseData<MRes_Notification>> Update(int id, MReq_Notification request);
        Task<ResponseData<int>> Delete(int id);
        Task<ResponseData<MRes_Notification>> GetById(int id);
        Task<ResponseData<List<MRes_Notification>>> GetAll();
        Task<ResponseData<List<MRes_Notification>>> GetListByEmployeeId(int employeeId);
    }
    public class S_Notification : IS_Notification
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_Notification(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_Notification>> Create(MReq_Notification request)
        {
            var res = new ResponseData<MRes_Notification>();
            try
            {
                var notification = _mapper.Map<Notification>(request);
                notification.CreatedAt = DateTime.Now;
                notification.UpdateAt = DateTime.Now;
                notification.IsRead ??= false;
                notification.Status ??= 1;

                _context.Notifications.Add(notification);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể tạo thông báo.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Notification>(notification);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<MRes_Notification>> Update(int id, MReq_Notification request)
        {
            var res = new ResponseData<MRes_Notification>();
            try
            {
                var notification = await _context.Notifications.FindAsync(id);
                if (notification == null)
                {
                    res.error.message = "Không tìm thấy thông báo.";
                    return res;
                }

                _mapper.Map(request, notification);
                notification.UpdateAt = DateTime.Now;

                _context.Notifications.Update(notification);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = "Không thể cập nhật.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Notification>(notification);
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
                var notification = await _context.Notifications.FindAsync(id);
                if (notification == null)
                {
                    res.error.message = "Không tìm thấy thông báo.";
                    return res;
                }

                _context.Notifications.Remove(notification);
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

        public async Task<ResponseData<MRes_Notification>> GetById(int id)
        {
            var res = new ResponseData<MRes_Notification>();
            try
            {
                var notification = await _context.Notifications
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (notification == null)
                {
                    res.error.message = "Không tìm thấy thông báo.";
                    return res;
                }

                res.data = _mapper.Map<MRes_Notification>(notification);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_Notification>>> GetAll()
        {
            var res = new ResponseData<List<MRes_Notification>>();
            try
            {
                var list = await _context.Notifications.AsNoTracking().ToListAsync();
                res.data = _mapper.Map<List<MRes_Notification>>(list);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.message = $"Exception: {ex.Message}";
            }

            return res;
        }

        public async Task<ResponseData<List<MRes_Notification>>> GetListByEmployeeId(int employeeId)
        {
            var res = new ResponseData<List<MRes_Notification>>();
            try
            {
                var list = await _context.Notifications
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .OrderByDescending(x => x.SentTime)
                    .ToListAsync();

                res.data = _mapper.Map<List<MRes_Notification>>(list);
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
