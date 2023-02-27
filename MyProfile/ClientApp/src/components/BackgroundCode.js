export const Code = `   \
    <span style="color: #236cdd">var</span> productInfoHeaderValue = 
    <span style="color: #236cdd">new</span> <span style="color: #bb80c1">ProductInfoHeaderValue</span>
    (<span style="color: #af8a5f">"NoThoughtsProfile"<span style="color: white">,</span> "1.0"</span>);
    <br/><br/>
    builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddSingleton</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">IGithubUserClient</span>
        , <span style="color:#bb80c1">GithubUserClient</span><span style="color: #af8a5f">></span>();<br/>
    builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddHttpClient</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">IGithubUserClient</span>
        , <span style="color:#bb80c1">GithubUserClient</span><span style="color: #af8a5f">></span>(client =><br/>
    {<br/>
        &emsp;client.<span style="color:#62bcc3">BaseAddress</span> = <span style="color: #236cdd">new</span>
         <span style="color:#bb80c1">Uri </span>(<span style="color:#bb80c1">AppConstants
         <span style="color: white">.</span>Client</span>.<span style="color: #65c2b0">GithubUserClient</span>);<br/>
        &emsp;client.<span style="color:#62bcc3">DefaultRequestHeaders<span style="color: white">.</span>UserAgent</span>
        .<span style="color:#36c68d">Add</span>(productInfoHeaderValue);<br/>
    });<br/>
    <br/>
    builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddSingleton</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">IGithubResourceClient</span>
        , <span style="color:#bb80c1">GithubResourceClient</span><span style="color: #af8a5f">></span>();<br/>
    builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddHttpClient</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">IGithubResourceClient</span>
        , <span style="color:#bb80c1">GithubResourceClient</span><span style="color: #af8a5f">></span>(client =><br/>
     {<br/>
        &emsp;client.<span style="color:#62bcc3">BaseAddress</span> = <span style="color: #236cdd">new</span>
         <span style="color:#bb80c1">Uri </span>(<span style="color:#bb80c1">AppConstants
         <span style="color: white">.</span>Client</span>.<span style="color: #65c2b0">GithubResourceClient</span>);<br/>
        &emsp;client.<span style="color:#62bcc3">DefaultRequestHeaders<span style="color: white">.</span>UserAgent</span>
        .<span style="color:#36c68d">Add</span>(productInfoHeaderValue);<br/>
    });<br/>
    <br/>
    builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddOptions</span>
    <span style="color: #af8a5f"><</span><span style="color:#bb80c1">UserConfiguration</span>
    <span style="color: #af8a5f">></span>()<br/>
        &emsp;.<span style="color:#36c68d">Bind</span>(builder.<span style="color:#62bcc3">Configuration</span>.
        <span style="color:#36c68d">GetSection</span>(<span style="color:#bb80c1">UserConfiguration</span>
        .<span style="color: #65c2b0">SectionName</span>))<br/>
        &emsp;.<span style="color:#36c68d">ValidateDataAnnotations</span>();<br/>
    <br/>
    <span style="color:#63ba69">// Добавление Background сервисов</span><br/>
    {<br/>
        &emsp;builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddHostedService</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">TimedProjectsUpdaterService</span>
        <span style="color: #af8a5f">></span>();<br/>
        &emsp;builder.<span style="color:#62bcc3">Services</span>.<span style="color:#36c68d">AddScoped</span>
        <span style="color: #af8a5f"><</span><span style="color:#bb80c1">IScopedProjectUpdaterService</span>, 
        <span style="color:#bb80c1">ScopedProjectsUpdaterService</span><span style="color: #af8a5f">></span>();<br/>
    }<br/>`