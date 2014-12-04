var RestService = { baseUrl: 'http://nominateandvoteservice.azurewebsites.net/api' };
var NominateAndVoteClient = angular.module('NominateAndVoteClient', ['ngRoute']);

NominateAndVoteClient.filter('nl2br', function () { return function (text) { return text.replace(/\n/g, '<br/>').replace(/\r/g, ''); }; });
NominateAndVoteClient.filter('unsafe', function ($sce) { return $sce.trustAsHtml; });

NominateAndVoteClient.config(function ($locationProvider, $routeProvider) {
    $routeProvider
        .when('/News', {
            controller: 'NewsController',
            templateUrl: 'views/news_list.html'
        })
        .when('/Polls', {
            controller: 'PollsController',
            templateUrl: 'views/polls_list.html'
        })
        .when('/ContactUs', {
            templateUrl: 'views/static_contact_us.html'
        })
        .when('/FAQ', {
            templateUrl: 'views/static_faq.html'
        })
        .otherwise({
            redirectTo: '/News'
        });
    /*.when('/edit/:hfid', {
        controller: 'EditCtrl',
        templateUrl: 'details.html'
    })*/

    $locationProvider.html5Mode(false).hashPrefix('!');
});

NominateAndVoteClient.controller('NewsController', ['$scope',
    function ($scope) {
        restServiceCall('GET', 'News', 'List', null, function (news) {
            $scope.$apply(function () { $scope.news = news; });
        });
    }]
);

NominateAndVoteClient.controller('PollsController', ['$scope',
    function ($scope) {
        $scope.polls = { nomination: [], voting: [], closed: [] };

        restServiceCall('GET', 'Poll', 'ListNominationPolls', null, function (nPolls) {
            $scope.$apply(function () { $scope.polls.nomination = nPolls; });
        });
        restServiceCall('GET', 'Poll', 'ListVotingPolls', null, function (vPolls) {
            $scope.$apply(function () { $scope.polls.voting = vPolls; });
        });
        restServiceCall('GET', 'Poll', 'ListClosedPolls', null, function (cPolls) {
            $scope.$apply(function () { $scope.polls.closed = cPolls; });
        });
    }]
);

/*
 *
    <button onclick="restServiceCall('GET', 'Admin', 'SearchUser', { term: 'Noe' }, function(data) { alert(data[0].name); })">Search user</button>
    <button onclick="restServiceCall('GET', 'News', 'List', null, function (data) { alert(data[2].title ); })">News</button>
 */