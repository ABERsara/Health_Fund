using Dal_Repository.Irepository; // ייבוא הממשק 'Irepository' לסיוע בהגדרת פעולות בסיסיות.
using Dal_Repository.models; // ייבוא המודלים (המחלקות של הנתונים) מהספריה 'Dal_Repository.models', כמו מחלקת Appointment.
using Microsoft.EntityFrameworkCore; // ייבוא של הספרייה 'EntityFrameworkCore', שמאפשרת אינטגרציה עם מסדי נתונים ועבודה באמצעות ORM (מיפוי אובייקטים למסדי נתונים).
using System; // ייבוא של מחלקות בסיסיות כמו סוגים מובנים (int, string) וחריגות מתוך הספרייה 'System'.
using System.Collections.Generic; // ייבוא של מחלקות עבור רשימות (List) ואוספים שונים מתוך הספרייה 'System.Collections.Generic'.
using System.Linq; // ייבוא של יכולות לביצוע שאילתות על אוספים באמצעות LINQ (שפת שאילתות על אובייקטים).
using System.Text; // ייבוא פונקציות לעבודה עם מחרוזות, כמו בניית מחרוזות בצורה יעילה.
using System.Threading.Tasks; // ייבוא תמיכה בקוד אסינכרוני, שמאפשר להריץ משימות בצורה מקבילית באמצעות Task.

namespace Dal_Repository.Repository
// הגדרת מרחב השמות 'Dal_Repository.Repository' - זהו "מקום" בקוד שבו המחלקות והפונקציות שלך נמצאות, עוזר למנוע התנגשות שמות בין מחלקות שונות.
{
    public class AppointmentsRepository : Irepository<Appointment>
    // מחלקה 'AppointmentsRepository' - מחלקה זו משמשת כמחלקת מאגר (Repository) לביצוע פעולות על ישויות של פגישות.

    {
        public async Task AddAsync(Appointment ap)
        // פונקציה אסינכרונית שמוסיפה פגישה חדשה למסד הנתונים. מקבלת אובייקט מסוג 'Appointment' כפרמטר.

        {
            try
            // פתיחת בלוק try, שמנסה להריץ קוד ועלול לזרוק חריגות.

            {
                using (MacabiContext db = new MacabiContext())
                // יצירת אובייקט 'MacabiContext' שבעזרתו מתחברים למסד הנתונים. ה'using' מבטיח שברגע שהפעולה תסתיים, האובייקט ישוחרר מהזיכרון.

                {
                    db.Appointments.Add(ap);
                    // הוספת הפגישה ('Appointment') שהתקבלה לפונקציה אל מסד הנתונים באמצעות ה-DbSet שנקרא 'Appointments'.

                    await db.SaveChangesAsync();
                    // שמירה של כל השינויים שבוצעו במסד הנתונים, בצורה אסינכרונית.

                }
            }
            catch { throw; }
            // אם יש חריגה כלשהי, הוא יזרוק אותה מחדש כדי שהקוד שמזמן את הפונקציה ידע שהתרחשה בעיה.
        }

        public async Task<List<Appointment>> SelectAllAsync()
        // פונקציה אסינכרונית שמחזירה רשימה של כל הפגישות הקיימות במסד הנתונים.

        {
            try
            // פתיחת בלוק try לטעינת הנתונים עם טיפול בחריגות אפשריות.

            {
                using (MacabiContext db = new MacabiContext())
                // יצירת אובייקט 'MacabiContext' שמחבר למסד הנתונים.

                {
                    // כולל את שדות הניווט:
                    return await db.Appointments
                    // מחזיר את כל הפגישות ממסד הנתונים, תוך הכללת הניווטים לאובייקטים קשורים.

                    .Include(a => a.DoctorNavigation)
                    // מוסיף לנתונים המוחזרים את הניווט לנתונים של הרופא.

                    .Include(a => a.MedicineNavigation)
                    // מוסיף לנתונים המוחזרים את הניווט לנתונים של התרופות.

                    .Include(a => a.PatientNavigation)
                    // מוסיף לנתונים המוחזרים את הניווט לנתונים של המטופל.

                    .ToListAsync();
                    // מבצע את השאילתה בצורה אסינכרונית ומחזיר רשימה של הפגישות.
                }
            }
            catch (Exception e)
            // במקרה של חריגה, ייכנס לחסימת ה-catch.

            {
                throw;
                // זורק מחדש את החריגה כדי שניתן יהיה לטפל בה ברמת הקוד שמזמן את הפונקציה.
            }
        }

        public async Task DeleteAsync(short id)
        // פונקציה אסינכרונית שמסירה פגישה על פי מזהה (ID) שסופק כפרמטר.

        {
            try
            {
                using (MacabiContext db = new MacabiContext())
                {
                    var found = await db.Appointments.FindAsync(id);
                    // חיפוש פגישה לפי מזהה (ID) והמתנה לתוצאה.

                    if (found != null)
                    // אם הפגישה נמצאה (לא null),

                    {
                        db.Appointments.Remove(found);
                        // מסירה את הפגישה שנמצאה ממסד הנתונים.

                        await db.SaveChangesAsync();
                        // שמירה של כל השינויים שבוצעו במסד הנתונים.
                    }
                }
            }
            catch (DbUpdateException e)
            // טיפול בחריגות עדכון ספציפיות, כאשר מתרחשת שגיאה בעדכון מסד הנתונים.

            {
                throw new Exception("כנראה שאתה מנסה למחוק פגישה שיש לה הזמנות. אם לא, שים לב לשגיאה: " + e.Message);
                // שגיאה בהתמודדות עם הפגישה, יש להיזהר במצב זה.
            }
            catch (Exception ex) { throw; }
            // טיפול בחריגות כלליות.
        }

        public async Task UpdateAsync(Appointment p, short id)
        // פונקציה אסינכרונית שמעדכנת פרטי פגישה לפי מזהה (ID) שסופק.

        {
            try
            {
                using (MacabiContext db = new MacabiContext())
                {
                    var found = await db.Appointments.FindAsync(id);
                    // חיפוש פגישה לפי מזהה (ID) והמתנה לתוצאה.

                    if (found != null)
                    // אם הפגישה נמצאה (לא null),

                    {
                        found.Patient = p.Patient;
                        found.Doctor = p.Doctor;
                        found.Medicine = p.Medicine;
                        // עדכון פרטי הפגישה עם המידע החדש.

                        await db.SaveChangesAsync();
                        // שמירה של כל השינויים שבוצעו במסד הנתונים.
                    }
                }
            }

            catch (Exception ex)
            { throw; }
            // טיפול בחריגות כלליות.
        }

    }
}
