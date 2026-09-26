# 🚀 Internal Asset Tracker — Progress & Technical Documentation

## 💡 Project Overview (فكرة المشروع)
**Internal Asset Tracker** هو نظام إدارة أصول وموارد داخلية للمؤسسات والشركات مبني باستخدام **ASP.NET Core MVC** و **Entity Framework Core** وقواعد بيانات **SQL Server**. 

يهدف النظام إلى إدارة وتتبع الأصول التقنية (مثل اللابتوبات والأجهزة) وإسنادها للموظفين والربط بين الأقسام، ويوفر إدارة كاملة (CRUD Operations) لأربعة كائنات رئيسية:
1. **Departments (الأقسام):** إدارة الهيكل التنظيمي للشركة.
2. **Employees (الموظفين):** إدارة بيانات الموظفين وربط كل موظف بقسمه المحدد (`DepartmentId`).
3. **Assets (الأصول):** إدارة أجهزة وأصول الشركة ومواصفاتها.
4. **Asset Assignments (تعيينات الأصول):** إدارة عمليات تسليم وإرجاع الأصول للموظفين، وتتبع تاريخ التعيين وحالة النشاط.

---

## 📅 Chronological Timeline & Issues Fixed (الخط الزمني والأخطاء المعالجة)

### 1️⃣ Phase 1: Core CRUD Implementation
* **Date:** September 2026
* **Completed:**
  * إنشائ قاعدة البيانات وتطوير الهيكل الأساسي للتحكم (Controllers) والواجهات (Views) لـ `Departments` و `Assets` و `Employees`.
  * إعداد العلاقات Foreign Keys بين `Employees` و `Departments`.
* **Issues Identified:**
  * عدم ظهور اسم القسم في شاشة جدول الموظفين `Index` وظهور رقم الـ `DepartmentId` الخالي من البيانات الوصفية بدلاً منه.

---

### 2️⃣ Phase 2: Asset Assignments & Relational Binding
* **Date:** September 25, 2026
* **Completed:**
  * إضافة `AssetAssignmentsController.cs` وشاشات العرض المرافقة (`Index`, `Create`, `Edit`, `Delete`).
* **Issues Identified:**
  * حدوث استثناء قاتل `Microsoft.Data.SqlClient.SqlException` عند محاولة حفظ التعيينات الجدد (`Cannot insert explicit value for identity column in table 'AssetAssignments' when IDENTITY_INSERT is set to OFF`).
  * ربط الواجهات بموديل الكائن الكامل `Asset` و `Employee` بدلاً من المعرفات الأجنبية `AssetId` و `EmployeeId`.

---

### 3️⃣ Phase 3: Identity Fixes, Navigation Loading & UI Enhancements
* **Date:** September 26, 2026
* **Completed Solutions:**
  * حل مشكلة قفزات الترقيم التلقائي (Identity Gaps) في الجداول بإنشاء **Row Counter** تسلسلي بالـ Razor.
  * حل مشكلة حظر الـ Identity Column بحذف حقل الـ `Id` المرئي من نموذج الإضافة `Create.cshtml`.
  * استبدال كائنات الـ Navigation Properties بحقول مفاتيح أجنبية وقوائم منسدلة `<select>` بالـ `SelectList`.
  * تحسين العرض بطلب البيانات المرتبطة استباقياً باستخدام `.Include()`.
  * تحسين تصميم أزرار العمليات (`Edit` / `Delete`) في جداول العرض باستخام Bootstrap (`btn-warning` و `btn-danger`) مع الحفاظ على ممررات الـ Route الصحيحة (`asp-route-id`).

---

## 🛠️ Code Changes & Modified Files (الأكواد والتعديلات)

### 1. Controllers

#### `EmployeesController.cs`
* **Target:** `Index()` Action
* **Reason:** تحميل بيانات القسم المترابطة لعرض الاسم بدلاً من قيمة الـ Null.
```csharp
public ActionResult Index()
{
    IEnumerable<Employee> employees = _db.Employees
        .Include(e => e.Department)
        .ToList();
    return View(employees);
}

// GET: AssetAssignments/Index
public IActionResult Index()
{
    var assignments = _db.AssetAssignments
        .Include(a => a.Asset)
        .Include(a => a.Employee)
        .ToList();
    return View(assignments);
}

// GET: AssetAssignments/Create
[HttpGet]
public IActionResult Create()
{
    ViewBag.AssetId = new SelectList(_db.Assets, "Id", "Name");
    ViewBag.EmployeeId = new SelectList(_db.Employees, "Id", "FullName");
    return View();
}

@{
    int rowNum = 1;
}

@foreach (var emp in Model)
{
    <tr>
        <td>@(rowNum++)</td>
        <td>@emp.FullName</td>
        <td>@emp.Email</td>
        <td>@(emp.Department?.Name ?? emp.DepartmentId.ToString())</td>
        <td>
            <a asp-action="Edit" asp-route-id="@emp.Id" class="btn btn-warning btn-sm">Edit</a>
            <a asp-action="Delete" asp-route-id="@emp.Id" class="btn btn-danger btn-sm">Delete</a>
        </td>
    </tr>
}

@model AssetAssignment

<form asp-action="Create" method="post">
    <!-- Removed Id input field to prevent SQL Identity Insert Exception -->

    <div class="form-group mb-3">
        <label asp-for="AssetId" class="control-label">Asset</label>
        <select asp-for="AssetId" class="form-control" asp-items="ViewBag.AssetId">
            <option value="">-- Select Asset --</option>
        </select>
        <span class="text-danger" asp-validation-for="AssetId"></span>
    </div>

    <div class="form-group mb-3">
        <label asp-for="EmployeeId" class="control-label">Employee</label>
        <select asp-for="EmployeeId" class="form-select" asp-items="ViewBag.EmployeeId">
            <option value="">-- Select Employee --</option>
        </select>
        <span class="text-danger" asp-validation-for="EmployeeId"></span>
    </div>

    <div class="form-group mb-3">
        <label asp-for="AssignedDate" class="control-label"></label>
        <input asp-for="AssignedDate" class="form-control" type="datetime-local" />
    </div>

    <div class="form-group mb-3">
        <label asp-for="ReturnDate" class="control-label"></label>
        <input asp-for="ReturnDate" class="form-control" type="datetime-local" />
    </div>

    <div class="form-group form-check mb-3">
        <input class="form-check-input" asp-for="IsActive" />
        <label class="form-check-label" asp-for="IsActive"></label>
    </div>

    <input type="submit" value="Create" class="btn btn-primary" />
</form>



@foreach (var item in Model)
{
    <tr>
        <td>@item.Id</td>
        <td>@item.Asset?.Name</td>
        <td>@item.Employee?.FullName</td>
        <td>@item.AssignedDate</td>
        <td>@item.ReturnDate</td>
        <td>@item.IsActive</td>
        <td>
            <a asp-action="Edit" asp-route-id="@item.Id" class="btn btn-warning btn-sm">Edit</a>
            <a asp-action="Delete" asp-route-id="@item.Id" class="btn btn-danger btn-sm">Delete</a>
        </td>
    </tr>
}

