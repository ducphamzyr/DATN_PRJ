﻿
using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Repositories
{
    public class AllRepositories<T> : IAllRepositories<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public AllRepositories(AppDbContext context, DbSet<T> dbSet)
        {
            var allowedTypes = new HashSet<Type>
            {
                typeof(DonHang),
                typeof(DonHangChiTiet),
                typeof(GioHang),
                typeof(GioHangChiTiet),
                typeof(MaGiamGia),
                typeof(NhanHieu),
                typeof(PhanLoai),
                typeof(PhuongThucThanhToan),
                typeof(SanPham),
                typeof(ThongBao)
            };

            if (!allowedTypes.Contains(typeof(T)))
            {
                throw new InvalidOperationException($"Type {typeof(T).Name} is not allowed.");
            }
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<bool> Create(T obj)
        {
            try
            {
                _dbSet.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(dynamic id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    return false;
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return false;
            }
        }

        public async Task<ICollection<T>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return new List<T>();
            }
        }

        public async Task<T> GetById(dynamic id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return null;
            }
        }

        public async Task<bool> Update(T obj)
        {
            try
            {
                _dbSet.Update(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return false;
            }
        }
    }
}
