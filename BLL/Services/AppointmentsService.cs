using BLL.Iservices; // ייבוא ממשק השירותים
using Dal_Repository.Irepository; // ייבוא ממשק המאגר
using Dal_Repository.models; // ייבוא המודלים (המחלקות של הנתונים)
using System; // ייבוא מחלקות בסיסיות כמו סוגים מובנים (int, string) וחריגות
using System.Collections.Generic; // ייבוא מחלקות עבור רשימות (List) ואוספים שונים
using System.Linq; // ייבוא יכולות לביצוע שאילתות על אוספים באמצעות LINQ
using System.Text; // ייבוא פונקציות לעבודה עם מחרוזות
using System.Threading.Tasks; // ייבוא תמיכה בקוד אסינכרוני
using AutoMapper; // ייבוא ספריית המיפוי אוטומטי
using DTO; // ייבוא המודלים של DTO

namespace BLL.Services
// הגדרת מרחב השמות 'BLL.Services' - זהו "מקום" בקוד שבו המחלקות והשירותים שלך נמצאים
{
    public class AppointmentsService : IAppointmentsService
    // מחלקת השירותים 'AppointmentsService' - משמשת כנקודת גישה לביצוע פעולות על פגישות
    {
        Dal_Repository.Irepository.Irepository<Appointment> idal; // משתנה עבור ממשק המאגר
        IMapper mapper; // משתנה עבור המ mapper

        public AppointmentsService(Irepository<Appointment> idal, IMapper mapper)
        // בנאי - מקבל את ממשק המאגר ואת המ mapper
        {
            this.idal = idal; // אתחול משתנה המאגר
            this.mapper = mapper; // אתחול משתנה ה mapper
        }

        public async Task AddAsync(AppointmentsDto p)
        // פונקציה אסינכרונית שמוסיפה פגישה חדשה
        {
            try
            {
                // המרה והוספת הפגישה באמצעות AutoMapper
                var appointment = mapper.Map<Appointment>(p); // המרה מה-DTO ל-Appointment
                await idal.AddAsync(appointment); // הוספת הפגישה דרך המאגר
            }
            catch (Exception ex) { throw; }
            // אם מתרחשת חריגה, היא תיזרק מחדש
        }

        public async Task<List<AppointmentsDto>> SelectAllAsync()
        // פונקציה אסינכרונית שמחזירה רשימה של כל הפגישות
        {
            try
            {
                var q1 = await idal.SelectAllAsync(); // קריאה לפונקציה לקבלת כל הפגישות
                // המרה בעזרת AutoMapper
                return mapper.Map<List<Appointment>, List<DTO.AppointmentsDto>>(q1);

            }
            catch (Exception ex) { throw; }
            // אם מתרחשת חריגה, היא תיזרק מחדש
        }

        public async Task UpdateAsync(AppointmentsDto p, short id)
        // פונקציה אסינכרונית לעדכון פגישה קיימת
        {
            try
            {
                // המרה והוספת הפגישה באמצעות AutoMapper
                var appointment = mapper.Map<Appointment>(p); // המרה מה-DTO ל-Appointment
                await idal.UpdateAsync(appointment, id); // קריאה לעדכן את הפגישה דרך המאגר
            }
            catch (Exception ex) { throw; }
            // אם מתרחשת חריגה, היא תיזרק מחדש
        }

        public async Task DeleteAsync(short id)
        // פונקציה אסינכרונית למחיקת פגישה
        {
            try
            {
                await idal.DeleteAsync(id); // קריאה למחוק את הפגישה דרך המאגר
            }
            catch (Exception ex) { throw; }
            // אם מתרחשת חריגה, היא תיזרק מחדש
        }

    }
}
