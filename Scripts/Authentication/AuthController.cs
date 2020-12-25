using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    [SerializeField] Text email, password, message;
    [SerializeField] FirebaseAuth auth;
    [SerializeField] SaveLoadManager slManager;
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        slManager = FindObjectOfType<SaveLoadManager>();


    }
    public void Login()
    {
        if (email.text == "" || password.text == "")
        {
            message.text = "Pease enter a valid Email and Password !!";
            return;
        }
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(
           task =>
           {
               if (task.IsCanceled)
               {
                   print("LogIn cancled");
                   message.text = "LogIn cancled";
                   return;
               }
               if (task.IsFaulted)
               {
                   print(task.Exception);
                   message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                   return;
               }
               if (task.IsCompleted)
               {
                   FirebaseUser user = task.Result;
                   print("User Logged In Successfully");
                   message.text = "Logged in as "+ user.Email;
               }
           });
    }


    public void Register()
    {
        if(email.text=="" || password.text=="")
        {
            message.text = "Pease enter a valid Email and Password !!";
            return;
        }
        int score = Random.RandomRange(0, 25);
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                {
                    print("Registeration cancled");
                    message.text = "Registeration cancled";
                    return;
                }
                if (task.IsFaulted)
                {
                    print(task.Exception);
                    message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                    return;
                }
                if (task.IsCompleted)
                {
                    FirebaseUser newUser = task.Result;
                    print("User Created Successfully");
                    slManager.SaveData(newUser.UserId,newUser.Email,score);
                    message.text = "User Created Successfully\nEmail: " + newUser.Email + "\nUserId: " + newUser.UserId;                   
                    return;
                }
            }
            );
    }
    public void AnonymousLogin()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                {
                    print("LogIn cancled");
                    message.text = "LogIn cancled";
                    return;
                }
                if (task.IsFaulted)
                {
                    print(task.Exception);
                    message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                    return;
                }
                if (task.IsCompleted)
                {
                    FirebaseUser anonymousUser = task.Result;
                    print("User Logged in anonymously");
                    message.text = "User Logged in anonymously\nUserId: " + anonymousUser.UserId;
                    //pd.SetPlayer(anonymousUser.UserId, "Ghost", 5);
                    return;
                }
            });
    }
    public void Logout()
    { if(auth.CurrentUser!=null)
        {
            print("User " + auth.CurrentUser.UserId + " Logged out");
            message.text = "User " + auth.CurrentUser.UserId + " Logged out";
            auth.SignOut();
        }
    }
    private void Update()
    {
        message.SetAllDirty(); 
    }
}
