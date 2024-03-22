using Tutorial4.Showcase.SingleResponsobility;

GovernmentService governmentService = new();
//WTF???
governmentService.GeneratePassport();

SingleAdminstrativeOfficeService adminstrativeOfficeService = new();
// LIKE 👍
adminstrativeOfficeService.GeneratePassport();