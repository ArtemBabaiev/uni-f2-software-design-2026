using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace BaseDotNetApp.Lecture12;

public class Delegates
{
    public static void Exec()
    {
        
    }

    public static void Bad()
    {
        var calc = new Calculator();
        MethodInfo method = typeof(Calculator).GetMethod("Multiply");

        // Недоліки: створення масиву object[], пакування (boxing) int в object
        object result = method.Invoke(calc, new object[] { 5, 10 });
    }

    public static void GoodViaDelegate()
    {
        var calc = new Calculator();
        MethodInfo method = typeof(Calculator).GetMethod("Multiply");

        // Створюємо делегат. Перший параметр Func — це тип екземпляра класу, наступні — аргументи.
        var multiplyDelegate = (Func<Calculator, int, int, int>)Delegate.CreateDelegate(
            typeof(Func<Calculator, int, int, int>), method);

        // !!! Створений делегат обов'язково треба кешувати
        
        // Наступні виклики надшвидкі, без boxing і перевірок:
        int result = multiplyDelegate(calc, 5, 10);
    }

    public static void GoodViaExressionTrees()
    {
        var calc = new Calculator();
        MethodInfo method = typeof(Calculator).GetMethod("Multiply");

        // 1. Описуємо параметри (екземпляр класу та аргументи)
        ParameterExpression instanceParam = Expression.Parameter(typeof(Calculator), "instance");
        ParameterExpression arg1Param = Expression.Parameter(typeof(int), "a");
        ParameterExpression arg2Param = Expression.Parameter(typeof(int), "b");

        // 2. Будуємо виклик методу
        MethodCallExpression callExpr = Expression.Call(instanceParam, method, arg1Param, arg2Param);

        // 3. Збираємо це в лямбда-вираз і компілюємо
        var lambda = Expression.Lambda<Func<Calculator, int, int, int>>(
            callExpr, instanceParam, arg1Param, arg2Param);

        Func<Calculator, int, int, int> compiledLambda = lambda.Compile(); // Цю операцію треба кешувати!

        // Швидкий виклик:
        int result = compiledLambda(calc, 5, 10);
    }

    public static void GoodViaEmit()
    {
        var calc = new Calculator();
        MethodInfo method = typeof(Calculator).GetMethod("Multiply");

        // Створюємо динамічний метод
        DynamicMethod dynamicMethod = new DynamicMethod(
            "FastMultiply", 
            typeof(int), 
            new[] { typeof(Calculator), typeof(int), typeof(int) });

        ILGenerator il = dynamicMethod.GetILGenerator();

        // Пишемо IL-код вручну
        il.Emit(OpCodes.Ldarg_0); // Завантажуємо екземпляр класу в стек (calc)
        il.Emit(OpCodes.Ldarg_1); // Завантажуємо перший аргумент (a)
        il.Emit(OpCodes.Ldarg_2); // Завантажуємо другий аргумент (b)
        il.Emit(OpCodes.Callvirt, method); // Викликаємо метод
        il.Emit(OpCodes.Ret); // Повертаємо результат

        var emitDelegate = (Func<Calculator, int, int, int>)dynamicMethod.CreateDelegate(
            typeof(Func<Calculator, int, int, int>));

        // Швидкий виклик:
        int result = emitDelegate(calc, 5, 10);
    }
}

public class Calculator
{
    public int Multiply(int a, int b) => a * b;
}