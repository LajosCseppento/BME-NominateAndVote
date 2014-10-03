<h1>BME-NominateAndVote</h1>

<p><em>Homework for the Software Architectures subject (BME, autumn of 2014)</em></p>

<h2>Introduction</h2>

<p>This project was our homework for the <a href="https://portal.vik.bme.hu/kepzes/targyak/VIAUM105/en/" target="_blank">Software Architectures subject</a> during our masters course at <a href="http://www.bme.hu/?language=en" target="_blank">Budapest University of Technology and Economics</a> (Budapest, Hungary). The goal of the homework was to develop a 3-layer architecture software using advanced design patterns in teams of 2.</p>

<h2>Features</h2>

<p>
  The project was aimed for 100 working hours, so 50 hours per team member, thus the list of features is not long. The domain was a simple application where users can nominate movies and series for polls and they can vote. The essential features are the following:
  
  <ul>
    <li>
      Three different roles:
      <ul>
        <li>Guest/Visitor</li>
        <li>User (when a guest logs in s/he becomes a user)</li>
        <li>Administrator (or simply admin)</li>
      </ul>
    </li>
    <li>
      Realisation of the whole nomination-voting workflow, which is the following:
      <ol type="1">
        <li>An administrator creates a new nomination-voting workflow</li>
        <li>Users can nominate movies/series for the vote</li>
        <li>After the nomination deadnile an administrator cleans the list of nominations</li>
        <li>Users can vote for the candidates</li>
        <li>The poll closes after deadline</li>
        <li>After a specified date, the result becomes public</li>
      </ol>
    </li>
    <li>Basic defense against cheating (login with Facebook account, administrator ability to ban suspicious accounts)</li>
    <li>News</li>
    <li>"Frequently Asked Questions" (FAQ) and "Contact Us" pages (static)</li>
  </ul>
</p>

TODO UC diagram

<h2>Technical background</h2>

<p>The application has three layers: a simple data layer, a REST service and an HTML5 client- The target infrastructure was Microsoft Azure. We used Visual Studio 2013 Ultimate (MSDNAA) along with MVC4 and Azure SDK 2.4 for development.</p>

TODO why azure? why not aws?

TODO write used cloud services in detail

TODO list other libraries/frameworks like angularjs, jquery, mvc4

TODO add architectural diagram
