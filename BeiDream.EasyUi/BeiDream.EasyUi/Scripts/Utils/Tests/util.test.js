///<reference path="~/Scripts/Utils/util.js"/>
///<reference path="~/Scripts/JQuery/jquery.min.js"/>
//判断是否包含
test("contains", function () {
    ok("a".contains() == false, "测试undefined");
    ok("a".contains(null) == false, "测试null");
    ok("a".contains("a"));
    ok("a b".contains(" "));
    ok("1Abc".contains("a"),"测试大小写");
    ok("a/".contains("a"));
    ok("/a".contains("a"));
    ok("a".contains("b") === false);
    ok("abc".contains("b"));
});

//判断起始匹配
test("startsWith", function () {
    ok("1abc".startsWith() == false, "测试undefined");
    ok("abc".startsWith("a"));
    ok("abc".startsWith("A"), "测试大小写");
    ok("1abc".startsWith("a") == false);
});

//判断结束匹配
test("endsWith", function () {
    ok("1abc".endsWith() == false, "测试undefined");
    ok("abc".endsWith("c"));
    ok("abc".endsWith("C"), "测试大小写");
    ok("1abc".endsWith("1") == false);
});

//从起始位置开始截断
test("trimStart", function () {
    equal("/a/b".trimStart("/a"), "/b");
    equal("".trimStart("a"), "");
    equal("/".trimStart("/"), "");
    equal("/".trimStart("/a"), "/");
});

//格式化日期
test("format", function() {
    var date = new Date("2014", "11", "30", "1", "20", "15");
    equal(date.format("yyyy-MM-dd HH:mm:ss"), "2014-12-30 01:20:15");
    equal(date.format("yyyy年MM月dd日 HH:mm:ss"), "2014年12月30日 01:20:15");
});

//数组是否包含指定元素
test("contains", function () {
    var list = ["a", "b"];
    ok(list.contains("a") === true);
    ok(list.contains("c") === false);
});

//移除数组指定元素
test("remove", function() {
    var list = ["a", "b"];
    list.remove("a");
    equal(list.length, 1);
    equal(list[0], "b");
    list.remove("c");
    equal(list.length, 1);
});