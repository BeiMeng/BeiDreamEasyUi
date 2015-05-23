///<reference path="~/Scripts/Utils/util.js"/>
///<reference path="~/Scripts/JQuery/jquery.min.js"/>
///<reference path="~/Scripts/Utils/jquery.util.js"/>
//生成Guid
test("newGuid", function () {
    ok($.newGuid().contains("-"));
    ok(!$.newGuid("").contains("-"));
});
//数值转换
test("toNumber", function () {
    equal($.toNumber("0"), 0);
    equal($.toNumber("1"), 1);
    equal($.toNumber("1.5"), 1.5);
    equal($.toNumber("1.5",0), 2);
    equal($.toNumber("a"), 0);
});
test("toNumber_四舍五入", function() {
    equal($.toNumber("8.99999999999999999", 0), 9);
    ok($.isNumber($.toNumber("8.99999999999999999", 0)) === true, typeof $.toNumber("8.99999999999999999", 0));
});
test("toNumber_截断", function () {
    equal($.toNumber("8.99999999999999999", 2, true) , 8.99);
    ok($.isNumber($.toNumber("8.99999999999999999", 2, true)) === true);
});
//是否空数组
test("isEmptyArray", function () {
    ok($.isEmptyArray() === false);
    ok($.isEmptyArray(null) === false);
    ok($.isEmptyArray([]) === true);
    ok($.isEmptyArray([1]) === false);
});
//格式化Iso日期
test("formatIsoDate", function() {
    equal($.formatIsoDate("2014-12-30T12:17:55.757"), "2014-12-30 12:17");
});
//连接url与参数
test("joinUrl", function() {
    equal($.joinUrl("http://www.a.com", "a=1"), "http://www.a.com?a=1");
    equal($.joinUrl("http://www.a.com?a=1", "b=2"), "http://www.a.com?a=1&b=2");
    equal($.joinUrl("http://www.a.com?a=1", { b: 2, c: 3 }), "http://www.a.com?a=1&b=2&c=3");
});
//移除url的参数
test("removeUrlParams", function () {
    equal($.removeUrlParams(""), "");
    equal($.removeUrlParams("/a/b"), "/a/b");
    equal($.removeUrlParams("/a/b?a=1"), "/a/b");
});
//将字符串转换为Json对象
test("toJsonObject", function () {
    equal($.toJsonObject(null), null);
    equal($.toJsonObject(undefined), undefined);
    var obj = $.toJsonObject({});
    ok($.isObject(obj) === true);
    obj = $.toJsonObject("{}");
    ok($.isObject(obj) === true);
    obj = $.toJsonObject("{A:1,B:'B'}");
    equal(obj.A, 1);
    equal(obj.B, "B");
});
//检查对象是否包含指定属性
test("containsProperty", function () {
    var obj = { a: 1 };
    equal($.containsProperty(obj, "a"), true);
    equal($.containsProperty(obj, "b"), false);
    equal($.containsProperty(), false);
    equal($.containsProperty(obj), false);
});