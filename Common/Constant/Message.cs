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

    public readonly static String MESSAGE_USER_ALREADY_VERIFY = "User has been verified!";
    public readonly static String MESSAGE_USER_VERIFY_CODE_INCORRECT = "Verify Code is incorrect, please make sure you have check mail";
    public readonly static String MESSAGE_USER_VERIFY_SUCCESSFUL = "User verify successful, please login again!";
    public readonly static String MESSAGE_USER_VERITY_FAIL = "User verify fail, please try later";
    #endregion

    #region LOGIN
    public readonly static String MESSAGE_USER_LOGIN_SUCCESSFUL = "login success";
    public readonly static String MESSAGE_USER_LOGIN_FAIL = "User login fail";
    public readonly static String MESSAGE_USER_lOGIN_WRONG_PASSWORD = "Wrong password! Please enter again";
    public readonly static String MESSAGE_USER_LOGIN_USER_UNVERIFY = "Account does not verify yet!";
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
    public readonly static String MESSAGE_FILE_DELETE_SUCCESSFUL = $"File has been deleted successful at time {DateTime.UtcNow}.";
    #endregion

    #region MANGA_MESSAGE

    public readonly static String MESSAGE_MANGA_DOES_NOT_EXIST = "Manga does not existed, please try double check it.";
    public readonly static String MESSAGE_MANGA_DELETE_SUCCESSFUL = $"Manga has been deleted successful at time {DateTime.UtcNow}.";
    public readonly static String MESSAGE_MANGA_DELETE_FAIL = $"Server fail to delete manga {DateTime.UtcNow},";
    public readonly static String MESSAGE_MANGA_CREATE_SUCCESSFUL = $"Manga has been created successful at time {DateTime.UtcNow}.";
    public readonly static String MESSAGE_MANGA_CREATE_FAIL = $"Server fail to create manga {DateTime.UtcNow},";
    public readonly static String MESSAGE_MANGA_ALREADY_DELETE = "Manga has been deleted already!";
    public readonly static String MESSAGE_MANGA_INVALID_FILE_EXTENSION = "Invalid manga image extension.";
    public readonly static String MESSAGE_MANGA_NO_DATA = "No data for display.";
    #endregion

    #region AUTHOR_MESSAGE

    public readonly static String MESSAGE_AUTHOR_DOES_NOT_EXIST = "Author does not existed, please try double check it.";
    public readonly static String MESSAGE_AUTHOR_NO_DATA = "No data for display.";
    public readonly static String MESSAGE_AUTHOR_DELETE_SUCCESSFUL = $"Author has been deleted successful at time {DateTime.UtcNow}";
    public readonly static String MESSAGE_AUTHOR_GET_SUCCESSUL = "Get Author successful.";

    #endregion

    #region JSON_HELPER
    public readonly static String MESSAGE_JSON_DESERIALIZE_FAIL = "Server fail to deserialize json data.";
    #endregion
}
