using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;

public static class Message
{
    #region USER
    public readonly static String VALIDATE_MESSAGE_USER_NOT_EXIST = "User does not exist!";
    public readonly static String VALIDATE_MESSAGE_USER_NAME_DUPLICATE = "User Name can not duplicate.";
    public readonly static String VALIDATE_MESSAGE_USER_EMAIL_DUPLICATE = "Email can not duplicate.";

    public readonly static String MESSAGE_USER_DELETE_SUCCESSFUL = "User has been deleted.";
    public readonly static String MESSAGE_USER_DELETE_FAIL = "Server get error when deleted user.";
    public readonly static String MESSAGE_USER_UPDATE_SUCCESSFUL = "User has been updated.";
    public readonly static String MESSAGE_USER_UPDATE_FAIL = "Server get error when updated user.";
    public readonly static String MESSAGE_USER_LIST_EMPTY = "No Data for display";
    public readonly static String MESSAGE_USER_GET_SUCCESSFUL = "Get users data success";
    public readonly static String MESSAGE_USER_CREATE_SUCCESSFUL = "Create new user sucess";
    public readonly static String MESSAGE_USER_CREATE_FAIL = "Create new user fail";
    #endregion

    #region LOGIN
    public readonly static String MESSAGE_USER_LOGIN_SUCCESSFUL = "login success";
    public readonly static String MESSAGE_USER_LOGIN_FAIL = "";
    public readonly static String MESSAGE_USER_lOGIN_WRONG_PASSWORD = "Wrong password! Please enter again";
    #endregion

    #region REGISTER
    public readonly static String MESSAGE_USER_REGISTER_SUCCESSFUL = "User register successful";
    public readonly static String MESSAGE_USER_REGISTER_FAIL = "Register new user fail";
    #endregion

    #region FILE_MESSAGE
    public readonly static String MESSAGE_FILE_SAVE_SUCCESSFUL = "File has been saved.";
    public readonly static String MESSAGE_FILE_SAVE_FAIL = "Server get error when save file.";
    public readonly static String MESSAGE_FILE_EXTENSION_INVALID = "File extension is invalid.";
    public readonly static String MESSAGE_FILE_NOT_FOUND = "File not found.";   
    public readonly static String MESSAGE_FILE_DELETE_SUCCESSFUL = "File has been deleted.";
    #endregion
}
