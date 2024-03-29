﻿
"use strict";
var KTPasswordResetNewPassword = function () {
    var e, t, a, s, r = function () {
        return 100 === s.getScore()
    };
    return {
        init: function () {
            e = document.querySelector("#kt_new_password_form"), t = document.querySelector("#kt_new_password_submit"), s = KTPasswordMeter.getInstance(e.querySelector('[data-kt-password-meter="true"]')), a = FormValidation.formValidation(e, {
                fields: {
                    Password: {
                        validators: {
                            notEmpty: {
                                message: "Şifre alanı boş geçilemez."
                            },
                            callback: {
                                message: "Geçerli bir şifre giriniz.",
                                callback: function (e) {
                                    if (e.value.length > 0) return r()
                                }
                            }
                        }
                    },
                    "RePassword": {
                        validators: {
                            notEmpty: {
                                message: "Şifre doğrulama alanı boş geçilemez."
                            },
                            identical: {
                                compare: function () {
                                    return e.querySelector('[name="Password"]').value
                                },
                                message: "Şifre eşleşmiyor."
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: !1
                        }
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            }), t.addEventListener("click", (function (r) {
                r.preventDefault(), a.revalidateField("Password"), a.validate().then((function (a) {
                    "Valid" == a ?
                        Swal.fire({
                            title: "Göndermek istediğinizden emin misiniz?",
                            icon: "warning",
                            showCancelButton: true,
                            cancelButtonText: "Hayır!",
                            confirmButtonText: "Evet!"
                        }).then(function (result) {
                            if (result.value) {
                                e.submit();
                            }
                        }) : Swal.fire({
                            text: "Lütfen eksik alanları doldurup tekrar deneyiniz.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Tamam",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                }))
            })), e.querySelector('input[name="Password"]').addEventListener("input", (function () {
                this.value.length > 0 && a.updateFieldStatus("password", "NotValidated")
            }))
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTPasswordResetNewPassword.init()
}));
